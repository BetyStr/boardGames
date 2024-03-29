using BusinessLayer.Mapper;
using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class OrderServiceTests
{
    
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
        .AddServices()
        .AddMockedDbContext();
    
    [Fact]
    public async Task GetOrdersAsync_ReturnsCorrectNumber()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
    
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
    
        // Act
        var result = await orderService.GetOrdersAsync(null, null, null, null, null, null, null, null);
        var orderDetails = result.ToList();
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(dbContext.Orders.Count(), orderDetails.Count);
    }
    
    
    [Fact]
    public async Task GetOrderByIdAsync_ExistingId_ReturnsOrder()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();        
        
        var orderToGet = dbContext.Orders.Include(order => order.User).First();
    
        // Act
        var result = (await orderService.GetOrderByIdAsync(orderToGet.Id)).Value;
    
        // Assert
        Assert.NotNull(result);
    
        Assert.Equal(orderToGet.TotalPrice, result.TotalPrice);
        Assert.Equal(orderToGet.Date, result.Date);
        Assert.NotNull(result.User);
        Assert.Equal(orderToGet.User.Name, result.User.Name);
    }
    
    [Fact]
    public async Task CreateOrderAsyncGetById_ReturnsOrder()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
    
        var newOrder = new OrderCreate
        {
            User = EntityMapper.MapModelToRelated(dbContext.Users.First()),
            TotalPrice = 15m,
            Books = new List<ModelRelated>{EntityMapper.MapModelToRelated(dbContext.Books.First())}
        };
    
        // Act
        var result = (await orderService.CreateOrderAsync(newOrder)).Value;
        var ret = (await orderService.GetOrderByIdAsync(result.Id)).Value;
        // Assert
        RunAsserts(ret, result);
    }
    
    [Fact]
    public async Task UpdateOrderAsync_ReturnsUpdatedOrder()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
       
        
        var orderToUpdate = dbContext.Orders.First();
        orderToUpdate.TotalPrice = 42m;
        
        // Act
        var result = (await orderService.UpdateOrderAsync(orderToUpdate.Id, new OrderUpdate
        {
            TotalPrice = orderToUpdate.TotalPrice
        })).Value;
        
        var ret = (await orderService.GetOrderByIdAsync(result.Id)).Value;
        // Assert
        RunAsserts(ret, result);
    }
    
    [Fact]
    public async Task DeleteOrderAsyncGetByIdDeleteAgain_ReturnsOrder()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
            
        var orderToDelete = dbContext.Orders.First();
            
        await orderService.DeleteOrderAsync(orderToDelete.Id);
    }
    
    private static void RunAsserts(OrderDetail? expected, OrderDetail? actual)
    {
        Assert.NotNull(actual);
        Assert.NotNull(expected);
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.TotalPrice, actual.TotalPrice);
        Assert.NotNull(actual.User);
        Assert.NotNull(expected.User);
        Assert.Equal(expected.User.Id, actual.User.Id);
        Assert.Equal(expected.User.Name, actual.User.Name);
    }
    
}