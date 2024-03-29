using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IRatingService
{
    Task<IEnumerable<RatingDetail>> GetRatingsAsync(int? userId, string? userName,
        int? bookId, string? bookName);
    Task<IEnumerable<RatingDetail>> GetSearchRatingsAsync(string? query);
    Task<Result<RatingDetail, (Error err, string message)>> GetRatingByIdAsync(int id);
    Task<Result<RatingDetail, (Error err, string message)>> CreateRatingAsync(RatingCreate ratingCreate);
    Task<Result<RatingDetail, (Error err, string message)>> UpdateRatingAsync(int id, RatingUpdate ratingUpdate);
    Task<Result<bool, (Error err, string message)>> DeleteRatingAsync(int id);
    Task<RatingDetail?> ExistRatingForUser(int userId, int bookId);
}