using BusinessLayer.Errors;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;


namespace BusinessLayer.Services;

public class RatingService : IRatingService
{
    private readonly BookHubDbContext _context;
    private readonly IMemoryCache _memoryCache;


    public RatingService(BookHubDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<RatingDetail>> GetRatingsAsync(int? userId, string? userName,
        int? bookId, string? bookName)
    {
        var ratings = _context.Ratings
            .Include(r => r.User)
            .Include(r => r.Book)
            .AsQueryable();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userName || u.Id == userId);
        if (user != null)
        {
            ratings = ratings.Where(r => r.User != null && r.User.Id == user.Id);
        }

        var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == bookName || b.Id == bookId);
        if (book != null)
        {
            ratings = ratings.Where(r => r.Book != null && r.Book.Id == book.Id);
        }

        var filteredRatings = await ratings.ToListAsync();
        return filteredRatings.Select(EntityMapper.MapRatingToRatingDetail);
    }

    public async Task<IEnumerable<RatingDetail>> GetSearchRatingsAsync(string? query)
    {
        var ratings = _context.Ratings
            .Include(r => r.User)
            .Include(r => r.Book)
            .AsQueryable();

        if (query != null)
        {
            ratings = ratings.Where(rating => rating.Book.Name.ToLower().Contains(query.ToLower()));
        }

        var result = await ratings.ToListAsync();
        return result.Select(EntityMapper.MapRatingToRatingDetail);
    }

    public async Task<Result<RatingDetail, (Error err, string message)>> GetRatingByIdAsync(int id)
    {
        var key = $"RatingById_{id}";
        if (_memoryCache.TryGetValue(key, out RatingDetail? cached) && cached is not null)
        {
            return cached;
        }

        var rating = await _context
            .Ratings
            .Include(r => r.User)
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (rating == null)
        {
            return ErrorMessages.RatingNotFound(id);
        }

        var mapped = EntityMapper.MapRatingToRatingDetail(rating);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));
        _memoryCache.Set(key, mapped, cacheEntryOptions);
        return mapped;
    }


    public async Task<Result<RatingDetail, (Error err, string message)>> CreateRatingAsync(RatingCreate ratingCreate)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == ratingCreate.User.Name
                                                                 || u.Id == ratingCreate.User.Id);
        if (user == null)
        {
            return ErrorMessages.UserNotFound(ratingCreate.User.Id, ratingCreate.User.Name);
        }

        var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == ratingCreate.Book.Name
                                                                 || b.Id == ratingCreate.Book.Id);
        if (book == null)
        {
            return ErrorMessages.BookNotFound(ratingCreate.Book.Id, ratingCreate.Book.Name);
        }

        var rating = new Rating
        {
            UserId = user.Id,
            User = user,
            BookId = book.Id,
            Book = book,
            Value = ratingCreate.Value,
            Comment = ratingCreate.Comment
        };
        _context.Ratings.Add(rating);
        UpdateOverallRatingValue(book, ratingCreate.Value);
        _memoryCache.Remove($"BookById_{book.Id}");
        await _context.SaveChangesAsync();
        return EntityMapper.MapRatingToRatingDetail(rating);
    }

    public async Task<Result<RatingDetail, (Error err, string message)>> UpdateRatingAsync(int id,
        RatingUpdate ratingUpdate)
    {
        var rating = await _context
            .Ratings
            .Include(r => r.Book)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (rating == null)
        {
            return ErrorMessages.RatingNotFound(id);
        }

        var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == rating.Book.Name
                                                                 || b.Id == rating.Book.Id);
        if (book == null)
        {
            return ErrorMessages.BookNotFound(rating.Book.Id, rating.Book.Name);
        }

        rating.Value = ratingUpdate.Value;
        UpdateOverallRatingValue(book, ratingUpdate.Value);

        if (!string.IsNullOrEmpty(ratingUpdate.Comment) && ratingUpdate.Comment != "string")
        {
            rating.Comment = ratingUpdate.Comment;
        }

        _memoryCache.Remove($"BookById_{book.Id}");
        await _context.SaveChangesAsync();
        return EntityMapper.MapRatingToRatingDetail(rating);
    }

    public async Task<Result<bool, (Error err, string message)>> DeleteRatingAsync(int id)
    {
        var rating = await _context.Ratings.FindAsync(id);
        if (rating == null)
        {
            return ErrorMessages.RatingNotFound(id);
        }

        _memoryCache.Remove($"BookById_{rating.BookId}");
        _context.Ratings.Remove(rating);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<RatingDetail?> ExistRatingForUser(int userId, int bookId)
    {
        var ratings = await GetRatingsAsync(userId, null, bookId, null);
        return ratings.FirstOrDefault();
    }

    private void UpdateOverallRatingValue(Book book, int value)
    {
        _memoryCache.Remove($"BookById_{book.Id}");
        var ratings = _context.Ratings.Where(r => r.BookId == book.Id).ToList();
        if (!ratings.IsNullOrEmpty())
        {
            book.OverallRating = (ratings.Sum(r => r.Value) + value) / (ratings.Count + 1);
        }
        else
        {
            book.OverallRating = value;
        }
    }
}