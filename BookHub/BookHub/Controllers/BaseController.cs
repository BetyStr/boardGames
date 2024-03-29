using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Errors;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class BaseController : Controller
{
    public IActionResult ErrorView((Error err, string message) error)
    {
        return View("ErrorView",
            new ErrorViewModel
                { Message = error.message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    protected bool TryGetUserId(out int id)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (claim is null)
        {
            id = 0;
            return false;
        }

        var ret = int.TryParse(claim, out var userId);
        if (!ret)
        {
            id = 0;
            return false;
        }

        id = userId;
        return true;
    }
}