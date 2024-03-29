using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class AuthorController : BaseController
{
    private readonly ILogger<AuthorController> _logger;
    private readonly IAuthorService _authorService;

    public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
    {
        _logger = logger;
        _authorService = authorService;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _authorService.GetAuthorsAsync(null, null, null);
        return View(authors);
    }
    
    public async Task<IActionResult> Search(string query)
    {
        var authors = await _authorService.GetSearchAuthorsAsync(query);
        return View("Index", authors);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(AuthorCreate model)
    {
        if (!ModelState.IsValid) return View(model);
        await _authorService.CreateAuthorAsync(model);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return author.Match(
            a => View(new AuthorUpdate
            {
                Name = a.Name
            }),
            ErrorView
        );
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, AuthorUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _authorService.UpdateAuthorAsync(id, model);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return author.Match(
            View,
            ErrorView
        );
    }
    
}