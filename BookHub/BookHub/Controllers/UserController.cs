using System.Text.Encodings.Web;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class UserController : BaseController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    private readonly IEmailSender _emailSender;



    public UserController(ILogger<UserController> logger, IUserService userService, UserManager<User> userManager, IEmailSender emailSender)
    {
        _logger = logger;
        _userService = userService;
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetUsersAsync();
        return View(users);    
    }
    
    public async Task<IActionResult> Search(string query)
    {
        var users = await _userService.GetSearchUsersAsync(query);
        return View("Index", users);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ResetPassword(int id)
    {
        var res = await _userService.GetUserAsync(id);
        if (!res.IsOk)
        {
            return ErrorView(res.Error);
        }

        var (token, user) = res.Value;
        var callbackUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { userId = id, code = token },
            protocol: Request.Scheme);
        await _emailSender.SendEmailAsync(
            user.Email,
            "Confirm your email",
            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
        return RedirectToAction("Index");
    }
    
    
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _userService.DeleteUserAsync(id);
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return user.Match(
            View,
            ErrorView);
    }
}