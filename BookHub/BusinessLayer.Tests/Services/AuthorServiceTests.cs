using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class AuthorServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
        .AddServices()
        .AddMockedDbContext();
    
        
    [Fact]
    public async Task GetAuthorsAsync_ReturnsCorrectNumber()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);

        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        // Act
        var result = await authorService.GetAuthorsAsync(null, null, null);
        var authorDetails = result.ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dbContext.Authors.Count(), authorDetails.Count);
    }
      [Fact]
    public async Task GetAuthorByIdAsync_ExistingId_ReturnsAuthor()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        var authorToGet = dbContext.Authors.First();

        // Act
        var result = (await authorService.GetAuthorByIdAsync(authorToGet.Id)).Value;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(authorToGet.Name, result.Name);
    }
        [Fact]
    public async Task CreateAuthorAsyncGetById_ReturnsAuthor()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();
            
        var newAuthor = new AuthorCreate
        {
            Name = "Talented Author"
        }; 
        // Act
        var result = await authorService.CreateAuthorAsync(newAuthor);
        var ret = (await authorService.GetAuthorByIdAsync(result.Id)).Value;
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(ret.Name, result.Name);
        Assert.Equal(ret.Id, result.Id);
    }
    [Fact]
    public async Task UpdateAuthorAsyncGetById_ReturnsGAuthor()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();
        
        var authorToUpdate = dbContext.Authors.First();

        authorToUpdate.Name = "Updated Author";
        
        // Act
        var result = (await authorService.UpdateAuthorAsync(authorToUpdate.Id, new AuthorUpdate
        {
            Name = authorToUpdate.Name
        })).Value;
        var ret = (await authorService.GetAuthorByIdAsync(result.Id)).Value;
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(ret.Name, result.Name);
        Assert.Equal(ret.Id, result.Id);
    }
    
    [Fact]
    public async Task DeleteAuthorAsyncGetByIdDeleteAgain_ReturnsAuthor()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();
            
        var authorToDelete = dbContext.Authors.First();
            
        await authorService.DeleteAuthorAsync(authorToDelete.Id);
    }

}