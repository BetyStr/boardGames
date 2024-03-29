using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IBookService
{
    Task<IEnumerable<BookDetail>> GetBooksAsync(string? bookName, int? genreId, string? genreName,
        int? publisherId, string? publisherName, int? authorId, string? authorName);

    Task<BookView> GetSearchBooksAsync(PaginationSettings? paginationSettings, string? query);

    Task<Result<BookDetail, (Error err, string message)>> GetBookByIdAsync(int id);
    Task<Result<BookDetail, (Error err, string message)>> CreateBookAsync(BookCreate bookCreate);
    Task<Result<BookDetail, (Error err, string message)>> UpdateBookAsync(int id, BookCreate bookUpdate);
    Task<Result<bool, (Error err, string message)>> DeleteBookAsync(int id);
}