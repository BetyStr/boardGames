using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDetail>>> GetAuthors(string? name, int? bookId, string? bookName)
        {
            try
            {
                return Ok(await _authorService.GetAuthorsAsync(name, bookId, bookName));
            }
            catch (Exception e)
            {
                return HandleAuthorException(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetail>> GetAuthorById(int id)
        {
           try
           {
               var author = await _authorService.GetAuthorByIdAsync(id);
               return author.Match<ActionResult<AuthorDetail>>(
                   a => Ok(a),
                   e => NotFound(e)
               );
           }
           catch (Exception e)
           {
               return HandleAuthorException(e);
           }
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDetail>> CreateAuthor(AuthorCreate authorCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                return Ok(await _authorService.CreateAuthorAsync(authorCreate));
            }
            catch (Exception e)
            {
                return HandleAuthorException(e);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDetail>> UpdateAuthor(int id, AuthorUpdate authorUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                var author = await _authorService.UpdateAuthorAsync(id, authorUpdate);
                return author.Match<ActionResult<AuthorDetail>> (
                    a => Ok(a),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleAuthorException(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorDetail>>DeleteAuthor(int id)
        {
            try
            {
                var res = await _authorService.DeleteAuthorAsync(id);
                return res.Match<ActionResult<AuthorDetail>>(
                    a => Ok(a),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleAuthorException(e);
            }
        }

        private ActionResult HandleAuthorException(Exception e)
        {
            return Problem("Unknown problem occured");
        }
    }
}