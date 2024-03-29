using BusinessLayer.Errors;
using BusinessLayer.Models;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Packaging;

namespace BusinessLayer.Services;

using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class BookService : IBookService
{
    private readonly BookHubDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public BookService(BookHubDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }


    public async Task<IEnumerable<BookDetail>> GetBooksAsync(string? bookName, int? genreId,
        string? genreName,
        int? publisherId, string? publisherName, int? authorId, string? authorName)
    {
        var books = _context.Books
            .Include(pg => pg.PrimaryGenre)
            .Include(g => g.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .AsQueryable();

        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreName || g.Id == genreId);
        if (genre != null)
        {
            books = books.Where(b => b.Genres.Contains(genre));
        }

        var publisher =
            await _context.Publishers.FirstOrDefaultAsync(p => p.Name == publisherName || p.Id == publisherId);
        if (publisher != null)
        {
            books = books.Where(b => b.Publisher.Id == publisher.Id);
        }

        var author = await _context
            .Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Name == authorName || a.Id == authorId);

        if (author != null)
        {
            books = books.Where(b => b.Authors.Contains(author));
        }

        if (!bookName.IsNullOrEmpty())
        {
            books = books.Where(b => b.Name == bookName);
        }

        var filteredBooks = await books.ToListAsync();
        return filteredBooks.Select(EntityMapper.MapBookToBookDetail);
    }

    public async Task<BookView> GetSearchBooksAsync(PaginationSettings? paginationSettings, string? query)
    {
        var books = _context.Books
            .Include(pg => pg.PrimaryGenre)
            .Include(g => g.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .AsQueryable();

        if (query != null)
        {
            books = books.Where(book => book.Name.ToLower().Contains(query.ToLower()));
        }

        if (paginationSettings != null)
        {
            var booksCount = await books.CountAsync();
            var pageCount = booksCount / paginationSettings.PageSize + int.Min( booksCount % paginationSettings.PageSize, 1);
            books = books
                .Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize)
                .Take(paginationSettings.PageSize);
            var result = await books.ToListAsync();
            return new BookView(result.Select(EntityMapper.MapBookToBookDetail),
                paginationSettings.PageNumber, pageCount);
        }
        var result2 = await books.ToListAsync(); 
        return new BookView(result2.Select(EntityMapper.MapBookToBookDetail),
                1, 1);

    }

    public async Task<Result<BookDetail, (Error err, string message)>> GetBookByIdAsync(int id)
    {
        var key = $"BookById_{id}";
        if (_memoryCache.TryGetValue(key, out BookDetail? cached) && cached is not null)
        {
            return cached;
        }

        var book = await _context
            .Books
            .Include(pg => pg.PrimaryGenre)
            .Include(g => g.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            return ErrorMessages.BookNotFound(id);
        }

        var mapped = EntityMapper.MapBookToBookDetail(book);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
        _memoryCache.Set(key, mapped, cacheEntryOptions);
        return mapped;
    }


    public async Task<Result<BookDetail, (Error err, string message)>> CreateBookAsync(BookCreate bookCreate)
    {
        if (bookCreate.Authors.IsNullOrEmpty())
        {
            return ErrorMessages.AuthorsEmpty();
        }
        
        if (bookCreate.Genres.IsNullOrEmpty())
        {
            return ErrorMessages.GenresEmpty();
        }

        var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == bookCreate.Publisher.Name
                                                                           || p.Id == bookCreate.Publisher.Id);
        if (publisher == null)
        {
            return ErrorMessages.PublisherNotFound(bookCreate.Publisher.Id, bookCreate.Publisher.Name);

        }

        var primaryGenre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == bookCreate.PrimaryGenre.Name
                                                                          || g.Id == bookCreate.PrimaryGenre.Id);

        if (primaryGenre == null)
        {
            return ErrorMessages.GenreNotFound(bookCreate.PrimaryGenre.Id, bookCreate.PrimaryGenre.Name);
        }

        var genreNames = bookCreate.Genres.Select(g => g.Name).ToHashSet();
        var genreIds = bookCreate.Genres.Select(g => g.Id).ToHashSet();
        var genres = await _context.Genres.Where(g => genreNames.Contains(g.Name) || genreIds.Contains(g.Id))
            .ToListAsync();

        if (genres.Count != bookCreate.Genres.Count)
        {
            return ErrorMessages.GenreNotFound();
        }

        var authorNames = bookCreate.Authors.Select(a => a.Name).ToHashSet();
        var authorIds = bookCreate.Authors.Select(a => a.Id).ToHashSet();
        var authors = await _context.Authors.Where(a => authorNames.Contains(a.Name) || authorIds.Contains(a.Id))
            .ToListAsync();

        if (authors.Count != bookCreate.Authors.Count)
        {
            return ErrorMessages.AuthorNotFound();
        }

        var book = new Book
        {
            Name = bookCreate.Name,
            Authors = authors,
            Genres = genres,
            Publisher = publisher,
            PublisherId = publisher.Id,
            PrimaryGenre = primaryGenre,
            PrimaryGenreId = primaryGenre.Id,
            Price = bookCreate.Price,
            StockInStorage = bookCreate.StockInStorage,
            OverallRating = bookCreate.OverallRating
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return EntityMapper.MapBookToBookDetail(book);
    }

    public async Task<Result<BookDetail, (Error err, string message)>> UpdateBookAsync(int id, BookCreate bookUpdate)
    {
        var book = await _context.Books
            .Include(pg => pg.PrimaryGenre)
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return ErrorMessages.BookNotFound(id);
        }

        book.Name = bookUpdate.Name;

        if (bookUpdate.Genres.Count > 0)
        {
            var genreNames = bookUpdate.Genres.Select(g => g.Name).ToHashSet();
            var genreIds = bookUpdate.Genres.Select(g => g.Id).ToHashSet();

            var genres = await _context.Genres
                .Where(g => genreNames.Contains(g.Name) || genreIds.Contains(g.Id))
                .ToListAsync();

            if (genres.Count != bookUpdate.Genres.Count)
            {
                return ErrorMessages.GenreNotFound();
            }

            book.Genres.Clear();
            book.Genres.AddRange(genres);
        }

        if (bookUpdate.Publisher.Name != "string")
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p =>
                p.Name == bookUpdate.Publisher.Name || p.Id == bookUpdate.Publisher.Id);
            if (publisher == null)
            {
                return ErrorMessages.PublisherNotFound(bookUpdate.Publisher.Id, bookUpdate.Publisher.Name);
            }

            book.Publisher = publisher;
            book.PublisherId = publisher.Id;
        }

        if (bookUpdate.PrimaryGenre.Name != "string")
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(p =>
                p.Name == bookUpdate.PrimaryGenre.Name || p.Id == bookUpdate.PrimaryGenre.Id);
            if (genre == null)
            {
                return ErrorMessages.GenreNotFound(bookUpdate.PrimaryGenre.Id, bookUpdate.PrimaryGenre.Name);

            }

            book.PrimaryGenre = genre;
            book.PrimaryGenreId = genre.Id;
        }


        book.Price = bookUpdate.Price;
        book.StockInStorage = bookUpdate.StockInStorage;
        book.OverallRating = bookUpdate.OverallRating;

        if (bookUpdate.Authors is { Count: > 0 })
        {
            var authorNames = bookUpdate.Authors.Select(a => a.Name).ToHashSet();
            var authorIds = bookUpdate.Authors.Select(a => a.Id).ToHashSet();

            var authors = await _context.Authors
                .Where(a => authorNames.Contains(a.Name) || authorIds.Contains(a.Id))
                .ToListAsync();

            if (authors.Count != bookUpdate.Authors.Count)
            {
                return ErrorMessages.AuthorNotFound();
            }

            book.Authors.Clear();
            book.Authors.AddRange(authors);
        }

        await _context.SaveChangesAsync();
        return EntityMapper.MapBookToBookDetail(book);
    }

    public async Task<Result<bool, (Error err, string message)>> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return ErrorMessages.BookNotFound(id);
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    private bool BookExists(int id)
    {
        return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
    }
    

    private void FixOverallRating()
    {
        foreach (var book in _context.Books.Include(book => book.Ratings))
        {
            if (!book.Ratings.IsNullOrEmpty()) book.OverallRating = (int)book.Ratings.Average(r => r.Value);
            else book.OverallRating = 0;
        }
        _context.SaveChanges();
    }
    
}