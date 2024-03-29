using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;


        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDetail>>> GetRatings(int? userId, string? userName,
            int? bookId, string? bookName)
        {
            try
            {
                return Ok(await _ratingService.GetRatingsAsync(userId, userName, bookId, bookName));
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDetail>> GetRatingById(int id)
        {
            try
            {
                var rating = await _ratingService.GetRatingByIdAsync(id);
                return rating.Match<ActionResult<RatingDetail>>(
                    r => Ok(r),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }


        [HttpPost]
        public async Task<ActionResult<RatingDetail>> CreateRating(RatingCreate ratingCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                var rating = await _ratingService.CreateRatingAsync(ratingCreate);
                return rating.Match<ActionResult<RatingDetail>>(
                    r => Ok(r),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RatingDetail>> UpdateRating(int id, RatingUpdate ratingDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                var rating = await _ratingService.UpdateRatingAsync(id, ratingDetail);
                return rating.Match<ActionResult<RatingDetail>>(
                    r => Ok(r),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RatingDetail>> DeleteRating(int id)
        {
            try
            {
                var res = await _ratingService.DeleteRatingAsync(id);
                return res.Match<ActionResult<RatingDetail>>(
                    r => Ok(r),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleRatingException(e);
            }
        }

        private ActionResult HandleRatingException(Exception e)
        {
            return Problem("Unknown problem occured");
        }
    }
}