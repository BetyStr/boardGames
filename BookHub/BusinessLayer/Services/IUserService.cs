using BusinessLayer.Errors;
using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services;

public interface IUserService
{
    Task<IEnumerable<UserDetail>> GetUsersAsync();
    Task<IEnumerable<UserDetail>> GetSearchUsersAsync(string? query);
    Task<Result<UserDetail, (Error err, string message)>> GetUserByIdAsync(int id);
    Task<Result<UserDetail, (Error err, string message)>> CreateUserAsync(UserCreate userCreate);
    Task<Result<UserDetail, (Error err, string message)>> UpdateUserAsync(int id, UserUpdate userUpdate);
    Task<Result<bool, (Error err, string message)>> DeleteUserAsync(int id);
    Task<Result<(string, User user), (Error err, string message)>> GetUserAsync(int id);

    Task<Result<bool, (Error err, string message)>> AddBookToWishlist(int id, int bookId);
    Task<Result<IEnumerable<BookDetail>, (Error err, string message)>> GetBooksInWishlist(int id);
    Task<Result<bool, (Error err, string message)>> DeleteBookFromWishlist(int userId, int bookId);
}