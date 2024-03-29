using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services;

public class UserServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private static readonly PasswordHasher<User> Hasher = new();

    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
        .AddServices()
        .AddMockedDbContext();

    public UserServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task GetUsersAsync_ReturnsCorrectNumber()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
    
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
    
        // Act
        var result = await userService.GetUsersAsync();
        var userDetails = result.ToList();
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(dbContext.Users.Count(), userDetails.Count);
    }
        
    [Fact]
    public async Task GetUserByIdAsync_ExistingId_ReturnsUser()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var userToGet = dbContext.Users.First();

        // Act
        var result = (await userService.GetUserByIdAsync(userToGet.Id)).Value;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userToGet.Name, result.Name);
    }
        
    [Fact]
    public async Task CreateUserAsyncGetById_ReturnsUser()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            
        var newUser = new UserCreate
        {
            Name = "Test",
            UserName = "test",
            Email = "test@test.com",
            Password = Hasher.HashPassword(null, "Aa123!"),
            Books = new List<ModelRelated>()
        }; 
        // Act
        var result = (await userService.CreateUserAsync(newUser)).Value;
        var ret = (await userService.GetUserByIdAsync(result.Id)).Value;
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(ret.Name, result.Name);
        Assert.Equal(ret.Id, result.Id);
    }
    
    [Fact]
    public async Task UpdateUserAsyncGetById_ReturnsUser()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        
        var userToUpdate = dbContext.Users.First();
    
        userToUpdate.Name = "Updated user";
        
        // Act
        var result = (await userService.UpdateUserAsync(userToUpdate.Id, new UserUpdate
        {
            Name = "Updated name",
            UserName = "updated username",
            Email = "updated mail",
            OldPassword = userToUpdate.PasswordHash,
            NewPassword = userToUpdate.PasswordHash
        })).Value;
        var ret = (await userService.GetUserByIdAsync(result.Id)).Value;
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(ret.Name, result.Name);
        Assert.Equal(ret.Id, result.Id);
    }
        
    [Fact]
    public async Task DeleteUserAsyncGetByIdDeleteAgain_ReturnsUser()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            
        var userToDelete = dbContext.Users.First();
            
        await userService.DeleteUserAsync(userToDelete.Id);
    }
}