using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class GenreController : BaseController
{
    private readonly ILogger<GenreController> _logger;
    private readonly IGenreService _genreService;

    public GenreController(ILogger<GenreController> logger, IGenreService genreService)
    {
        _logger = logger;
        _genreService = genreService;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _genreService.GetGenresAsync(null);
        return View(genres);
    }
    
    public async Task<IActionResult> Search(string query)
    {
        var genres = await _genreService.GetSearchGenresAsync(query);
        return View("Index", genres);
    }


    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(GenreCreate model)
    {
        if (!ModelState.IsValid) return View(model);
        await _genreService.CreateGenreAsync(model);
        return RedirectToAction("Index");
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        return genre.Match(
            g => View(new GenreCreate()
            {
                Name = g.Name,
            }),
            ErrorView);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, GenreCreate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _genreService.UpdateGenreAsync(id, model);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _genreService.DeleteGenreAsync(id);
        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        return genre.Match(
            View,
            ErrorView);
    }
}