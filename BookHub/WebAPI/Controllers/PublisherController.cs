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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDetail>>> GetPublishers(string? name)
        {
            try
            {
                return Ok(await _publisherService.GetPublishersAsync(name));
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDetail>> GetPublisherById(int id)
        {
            try
            {
                var publisher = await _publisherService.GetPublisherByIdAsync(id);
                return publisher.Match<ActionResult<PublisherDetail>>(
                    p => Ok(p),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            }        
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDetail>> CreatePublisher(PublisherCreate publisherCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                return Ok(await _publisherService.CreatePublisherAsync(publisherCreate));
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            } 
           
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<PublisherDetail>> UpdatePublisher(int id, PublisherUpdate publisherUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                var publisher = await _publisherService.UpdatePublisherAsync(id, publisherUpdate);
                return publisher.Match<ActionResult<PublisherDetail>>(
                    p => Ok(p),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            } 
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<PublisherDetail>> DeletePublisher(int id)
        {
            try
            {
                var res = await _publisherService.DeletePublisherAsync(id);
                return res.Match<ActionResult<PublisherDetail>>(
                    p => Ok(p),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandlePublisherException(e);
            } 
        }
        
        private ActionResult HandlePublisherException(Exception e)
        {
            return Problem("Unknown problem occured");
        }
    }
}
