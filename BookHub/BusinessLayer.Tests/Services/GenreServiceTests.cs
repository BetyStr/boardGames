using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services;

public class GenreServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
        .AddServices()
        .AddMockedDbContext();

    public GenreServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
        
    [Fact]
    public async Task GetGenresAsync_ReturnsCorrectNumber()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);

        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();

        // Act
        var result = await genreService.GetGenresAsync(null);
        var genreDetails = result.ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dbContext.Genres.Count(), genreDetails.Count);
    }
        
    [Fact]
    public async Task GetGenreByIdAsync_ExistingId_ReturnsGenre()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();

        var genreToGet = dbContext.Genres.First();

        // Act
        var result = (await genreService.GetGenreByIdAsync(genreToGet.Id)).Value;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(genreToGet.Name, result.Name);
    }
        
    [Fact]
    public async Task CreateGenreAsyncGetById_ReturnsGenre()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();
            
        var newGenre = new GenreCreate
        {
            Name = "Awesome new genre"
        }; 
        // Act
        var result = await genreService.CreateGenreAsync(newGenre);
        var ret = (await genreService.GetGenreByIdAsync(result.Id)).Value;
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(ret.Name, result.Name);
        Assert.Equal(ret.Id, result.Id);
    }
    
    [Fact]
    public async Task UpdateGenreAsyncGetById_ReturnsGenre()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();
        
        var genreToUpdate = dbContext.Genres.First();

        genreToUpdate.Name = "Updated genre";
        
        // Act
        var result = (await genreService.UpdateGenreAsync(genreToUpdate.Id, new GenreCreate
        {
            Name = genreToUpdate.Name
        })).Value;
        var ret = (await genreService.GetGenreByIdAsync(result.Id)).Value;
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(ret.Name, result.Name);
        Assert.Equal(ret.Id, result.Id);
    }
        
    [Fact]
    public async Task DeleteGenreAsyncGetByIdDeleteAgain_ReturnsGenre()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();
            
        var genreToDelete = dbContext.Genres.First();
            
        await genreService.DeleteGenreAsync(genreToDelete.Id);
    }
}