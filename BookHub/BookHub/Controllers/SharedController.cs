using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class SharedController : BaseController
{
    // GET
    private readonly ILogger<SharedController> _logger;

    public SharedController(ILogger<SharedController> logger)
    {
        _logger = logger;
    }

    public IActionResult Privacy()
    {
        return View();
    }
}