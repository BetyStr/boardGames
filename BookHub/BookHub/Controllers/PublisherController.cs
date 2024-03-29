using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class PublisherController : BaseController
{
    private readonly ILogger<PublisherController> _logger;
    private readonly IPublisherService _publisherService;

    public PublisherController(ILogger<PublisherController> logger, IPublisherService publisherService)
    {
        _logger = logger;
        _publisherService = publisherService;
    }

    public async Task<IActionResult> Index()
    {
        var publishers = await _publisherService.GetPublishersAsync(null);
        return View(publishers);
    }
    
    public async Task<IActionResult> Search(string query)
    {
        var publishers = await _publisherService.GetSearchPublishersAsync(query);
        return View("Index", publishers);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(PublisherCreate model)
    {
        if (!ModelState.IsValid) return View(model);
        await _publisherService.CreatePublisherAsync(model);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var publisher = await _publisherService.GetPublisherByIdAsync(id);
        return publisher.Match(
            p => View(new PublisherUpdate
            {
                Name = p.Name,
                Books = p.Books
            }),
            ErrorView
        );
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, PublisherUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _publisherService.UpdatePublisherAsync(id, model);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _publisherService.DeletePublisherAsync(id);
        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var publisher = await _publisherService.GetPublisherByIdAsync(id);
        return publisher.Match(
            View,
            ErrorView);
    }
}