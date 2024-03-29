using BusinessLayer.Mapper;
using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services;

public class BookServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder =
        new MockedDependencyInjectionBuilder()
            .AddServices()
            .AddMockedDbContext();

    public BookServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetBooksAsync_ReturnsCorrectNumber()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);

        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        // Act
        var result = await bookService.GetBooksAsync(null, null, null, null, null, null, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dbContext.Books.Count(), result.Count());
    }

    [Fact]
    public async Task GetBookByIdAsync_ExistingId_ReturnsBook()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        var bookToGet = dbContext.Books.Include(book => book.Authors).Include(book => book.Genres).First();

        // Act
        var result = (await bookService.GetBookByIdAsync(bookToGet.Id)).Value;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bookToGet.Name, result.Name);
        Assert.Equal(bookToGet.Id, result.Id);
        Assert.Equal(bookToGet.Genres.Count, result.Genres.Count);
        Assert.Equal(bookToGet.Authors.Count, result.Authors.Count);
    }

    [Fact]
    public async Task CreateBookAsync_ReturnsNewBook()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        var bookCreate = new BookCreate
        {
            Name = "Eragon",
            Publisher = EntityMapper.MapModelToRelated(dbContext.Publishers.First()),
            Price = 10.99m,
            StockInStorage = 100,
            OverallRating = 50,
            PrimaryGenre = EntityMapper.MapModelToRelated(dbContext.Genres.First()),
            Genres = dbContext.Genres.Select(EntityMapper.MapModelToRelated).ToList(),
            Authors = dbContext.Genres.Select(EntityMapper.MapModelToRelated).ToList(),
        };
        // Act
        var result = (await bookService.CreateBookAsync(bookCreate)).Value;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bookCreate.Genres.Count, result.Genres.Count);
        Assert.Equal(bookCreate.Authors.Count, result.Authors.Count);
        Assert.Equal(bookCreate.Name, result.Name);
        Assert.Equal(bookCreate.Price, result.Price);
        Assert.Equal(bookCreate.StockInStorage, result.StockInStorage);
        Assert.Equal(bookCreate.OverallRating, result.OverallRating);
    }

    [Fact]
    public async Task UpdateBookAsync_ReturnsUpdatedBook()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        var bookToUpdate = dbContext.Books.Include(book => book.Publisher).Include(book => book.Authors)
            .Include(book => book.Genres).First();
        var bookUpdate = new BookCreate()
        {
            Name = "Update book name",
            PrimaryGenre = EntityMapper.MapModelToRelated(dbContext.Genres.Last()),
            Genres = dbContext.Genres.Select(EntityMapper.MapModelToRelated).Where(g => g.Id % 2 == 0).ToList(),
            Publisher = EntityMapper.MapModelToRelated(dbContext.Publishers.Last()),
            StockInStorage = 10,
            Price = 32,
            Authors = dbContext.Authors.Select(EntityMapper.MapModelToRelated).Where(a => a.Id % 2 == 0).ToList(),
            OverallRating = 0
        };
        // Act
        var result = (await bookService.UpdateBookAsync(bookToUpdate.Id, bookUpdate)).Value;
        var ret = (await bookService.GetBookByIdAsync(result.Id)).Value;
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(bookUpdate.Authors.Count, result.Authors.Count);
        Assert.Equal(bookUpdate.Genres.Count, result.Genres.Count);
        Assert.Equal(bookUpdate.Name, result.Name);
        Assert.Equivalent(bookUpdate.Publisher, result.Publisher);
        Assert.Equal(ret.Id, result.Id);
        Assert.Equal(ret.Authors.Count, result.Authors.Count);
        Assert.Equal(ret.Genres.Count, result.Genres.Count);
        Assert.Equal(ret.Name, result.Name);
        Assert.Equivalent(ret.Publisher, result.Publisher);
    }

    [Fact]
    public async Task DeleteBookAsyncGetByIdDeleteAgain_ReturnsRating()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

        var bookToDelete = dbContext.Books.Include(book => book.Publisher).Include(book => book.Authors).Include(book => book.Genres).First();

        await bookService.DeleteBookAsync(bookToDelete.Id);
    }
}