using System.Security.Claims;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Errors;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class BookController : BaseController
{
    private readonly ILogger<BookController> _logger;
    private readonly IBookService _bookService;
    private readonly IRatingService _ratingService;
    private readonly IUserService _userService;

    public BookController(ILogger<BookController> logger, IBookService bookService, IRatingService ratingService,
        IUserService userService)
    {
        _logger = logger;
        _bookService = bookService;
        _ratingService = ratingService;
        _userService = userService;
    }
    [HttpGet("{page:int}")]
    public async Task<IActionResult> Index(int? page = 1)
    {
        var paginationSetting = new PaginationSettings(10, page ?? 1);
        var books = await _bookService.GetSearchBooksAsync(paginationSetting, null);
        return View(books);
    }
    [HttpGet("{page:int}")]
    public async Task<IActionResult> Search(string query, int? page)
    {
        var paginationSetting = new PaginationSettings(10, page ?? 1);
        var books = await _bookService.GetSearchBooksAsync(paginationSetting, query);
        return View("Index", books);
    }


    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(BookCreate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var book = await _bookService.CreateBookAsync(model);
        if (book.IsOk)
        {
            return RedirectToAction("Index");
        }

        switch (book.Error.err)
        {
            case Error.GenreNotFound or Error.MultipleGenresNotFound:
                ModelState.AddModelError(nameof(model.PrimaryGenre.Name),
                    "Genre does not exist, you must create it first");
                break;
            case Error.PublisherNotFound:
                ModelState.AddModelError(nameof(model.Publisher.Name),
                    "Publisher does not exist, you must create it first");
                break;
            case Error.AuthorNotFound or Error.MultipleAuthorsNotFound or Error.AuthorFieldEmpty:
                ModelState.AddModelError(nameof(model.Authors), "Authors do not exist, you must create it first");
                break;
            case Error.GenreNotFound or Error.MultipleGenresNotFound or Error.GenreFieldEmpty:
                ModelState.AddModelError(nameof(model.Genres), "Genres do not exist, you must create it first");
                break;
        }

        return View(model);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var bookRes = await _bookService.GetBookByIdAsync(id);
        return bookRes.Match(
            book => View(new BookCreate()
            {
                Name = book.Name,
                PrimaryGenre = book.PrimaryGenre,
                Genres = book.Genres,
                Publisher = book.Publisher,
                StockInStorage = book.StockInStorage,
                OverallRating = 0,
                Price = book.Price,
                Authors = book.Authors
            }),
            ErrorView);
    }


    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, BookCreate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _bookService.UpdateBookAsync(id, model);
        return RedirectToAction("Search", "Book", new { id = 1 });
    }

    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _bookService.DeleteBookAsync(id);
        return RedirectToAction("Search", "Book", new { id = 1 });
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return book.Match(
            View,
            ErrorView);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ShowRatings(int id)
    {
        var reviews = await _ratingService.GetRatingsAsync(null, null, id, null);
        return PartialView("_RatingsPartial", reviews);
    }

    [Authorize]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> AddToWishlist(int id)
    {
        if (!TryGetUserId(out var userId))
        {
            return ErrorView((Error.UserNotFound, "User not logged in"));
        }
        var user = await _userService.GetUserByIdAsync(userId);
        if (!user.IsOk)
        {
            return ErrorView(user.Error);
        }

        var result = await _userService.AddBookToWishlist(user.Value.Id, id);
        if (result is { IsOk: true, Value: true })
        {
            TempData["WishlistMessage"] = "Book added to Wishlist";
        }
        else
        {
            TempData["WishlistMessage"] = "Book already in Wishlist";
        }

        return RedirectToAction("Detail", new { id = id });
    }

    [Authorize]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> AddRating(int id, int value, string? comment)
    {
        if (!TryGetUserId(out var userId))
        {
            return ErrorView((Error.UserNotFound, "User not logged in"));
        }
        var user = (await _userService.GetUserByIdAsync(userId));
        var book = (await _bookService.GetBookByIdAsync(id));
        if (!user.IsOk)
        {
            return ErrorView(user.Error);
        }

        if (!book.IsOk)
        {
            return ErrorView(book.Error);
        }

        var rating = await _ratingService.ExistRatingForUser(user.Value.Id, book.Value.Id);
        if (rating != null)
        {
            ViewBag.ModifyRatingConfirmation = true;
            ViewBag.ExistingRatingId = rating.Id;
            return RedirectToAction("Detail", new { id = id });
        }

        var newRating = new RatingCreate
        {
            User = EntityMapper.MapUserDetailToRelated(user.Value),
            Book = EntityMapper.MapBookDetailToRelated(book.Value),
            Value = value,
            Comment = comment,
        };
        var result = await _ratingService.CreateRatingAsync(newRating);
        return result.Match(
            _ => RedirectToAction("Detail", new { id = id }),
            ErrorView);
    }


    public async Task<IActionResult> CheckRatingExists(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int.TryParse(userIdClaim, out int userId);

        var user = (await _userService.GetUserByIdAsync(userId));
        var book = (await _bookService.GetBookByIdAsync(id));
        if (!user.IsOk)
        {
            return ErrorView(user.Error);
        }

        if (!book.IsOk)
        {
            return ErrorView(book.Error);
        }

        var rating = await _ratingService.ExistRatingForUser(user.Value.Id, book.Value.Id);

        if (rating != null)
        {
            var editUrl = Url.Action("Edit", "Rating", new { id = rating.Id });
            return Json(new { ratingExists = true, editUrl });
        }

        return Json(new { ratingExists = false });
    }
}