using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDetail>> GetAuthorsAsync(string? name, int? bookId, string? bookName);
    Task<IEnumerable<AuthorDetail>> GetSearchAuthorsAsync(string? query);
    Task<Result<AuthorDetail, (Error err, string message)>> GetAuthorByIdAsync(int id);
    Task<AuthorDetail> CreateAuthorAsync(AuthorCreate authorCreate);
    Task<Result<AuthorDetail, (Error err, string message)>> UpdateAuthorAsync(int id, AuthorUpdate authorUpdate);
    Task<Result<bool, (Error err, string message)>> DeleteAuthorAsync(int id);
}