using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDetail>>> GetGenres(string? name)
        {
            try
            {
                return Ok(await _genreService.GetGenresAsync(name));
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDetail>> GetGenreById(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreByIdAsync(id);
                return genre.Match<ActionResult<GenreDetail>>(
                    g => Ok(g),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GenreDetail>> CreateGenre(GenreCreate genreCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            
            try
            {
                return Ok(await _genreService.CreateGenreAsync(genreCreate));
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
            
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GenreDetail>> UpdateGenre(int id, GenreCreate genreDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                var genre = await _genreService.UpdateGenreAsync(id, genreDetail);
                return genre.Match<ActionResult<GenreDetail>>(
                    g => Ok(g),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreDetail>> DeleteGenre(int id)
        {
            try
            {
                var res = await _genreService.DeleteGenreAsync(id);
                return res.Match<ActionResult<GenreDetail>>(
                    g => Ok(g),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
        }

        private ActionResult HandleGenresException(Exception e)
        {
            return Problem("Unknown problem occured");
        }
    }
}