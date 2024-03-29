using BusinessLayer.Mapper;
using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services;

public class RatingServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
        .AddServices()
        .AddMockedDbContext();

    public RatingServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetRatingsAsync_ReturnsCorrectNumber()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);

        var ratingService = scope.ServiceProvider.GetRequiredService<IRatingService>();

        // Act
        var result = await ratingService.GetRatingsAsync(1, null, 2, null);
        var ratingDetails = result.ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(
            dbContext.Ratings
                .Count(r => r.User != null && r.Book != null && r.User.Id == 1 && r.Book.Id == 2),
            ratingDetails.Count);
    }

    [Fact]
    public async Task GetRatingByIdAsync_ExistingId_ReturnsRating()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var ratingService = scope.ServiceProvider.GetRequiredService<IRatingService>();

        var ratingToGet = dbContext.Ratings.Include(rating => rating.Book).Include(rating => rating.User).First();

        // Act
        var result = (await ratingService.GetRatingByIdAsync(ratingToGet.Id)).Value;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ratingToGet.Id, result.Id);
        Assert.Equal(ratingToGet.Book?.Name, result.Book.Name);
        Assert.Equal(ratingToGet.User?.Name, result.User.Name);
    }

    [Fact]
    public async Task CreateRatingAsyncGetById_ReturnsRating()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var ratingService = scope.ServiceProvider.GetRequiredService<IRatingService>();
        var user = EntityMapper.MapModelToRelated(dbContext.Users.First());
        var book = EntityMapper.MapModelToRelated(dbContext.Books.First());
        var newRating = new RatingCreate
        {
            User = user,
            Book = book,
            Value = 43,
            Comment = "My little dog Bobi excavates graves"
        };
        // Act
        var result = (await ratingService.CreateRatingAsync(newRating)).Value;
        var ret = (await ratingService.GetRatingByIdAsync(result.Id)).Value;
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(ret.Id, result.Id);
        Assert.Equal(ret.Book.Name, result.Book.Name);
        Assert.Equal(ret.User.Name, result.User.Name);
        Assert.Equal(ret.Comment, result.Comment);
    }

    [Fact]
    public async Task UpdateRatingAsyncGetById_ReturnsRating()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var ratingService = scope.ServiceProvider.GetRequiredService<IRatingService>();

        var ratingToUpdate = dbContext.Ratings.Include(rating => rating.User).Include(rating => rating.Book).First();

        ratingToUpdate.Comment = "Updated rating";

        // Act
        var result = (await ratingService.UpdateRatingAsync(ratingToUpdate.Id, new RatingUpdate
        {
            Value = 100,
            Comment = ratingToUpdate.Comment,
        })).Value;
        var ret = (await ratingService.GetRatingByIdAsync(result.Id)).Value;
        // Assert
        // Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(result.Comment, ret.Comment);
        Assert.Equal(result.Id, ret.Id);
    }

    [Fact]
    public async Task DeleteRatingAsyncGetByIdDeleteAgain_ReturnsRating()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var ratingService = scope.ServiceProvider.GetRequiredService<IRatingService>();

        var ratingToDelete = dbContext.Ratings.Include(rating => rating.User).Include(rating => rating.Book).First();

        await ratingService.DeleteRatingAsync(ratingToDelete.Id);
    }
}