using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreDetail>> GetGenresAsync(string? name);
    Task<IEnumerable<GenreDetail>> GetSearchGenresAsync(string? query);
    Task<Result<GenreDetail, (Error err, string message)>> GetGenreByIdAsync(int id);
    Task<GenreDetail> CreateGenreAsync(GenreCreate genreCreate);
    Task<Result<GenreDetail, (Error err, string message)>> UpdateGenreAsync(int id, GenreCreate genreUpdate);
    Task<Result<bool, (Error err, string message)>> DeleteGenreAsync(int id);
}