using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthToken>> Login(UserSignIn userSignIn)
    {
        try
        {
            return Ok(await _authService.Login(userSignIn));

        }
        catch (Exception e)
        {
            return HandleAuthorizationException(e);
        }
    }
    

    private ActionResult HandleAuthorizationException(Exception e)
    {
        return e switch
        {
            UserNotFoundException => NotFound(e.Message),
            UnauthorizedApiAccess => Unauthorized(),
            _ => Problem("Unknown problem occured")
        };
    }
}