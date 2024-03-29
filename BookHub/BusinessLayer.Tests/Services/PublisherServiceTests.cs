using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services
{
    public class PublisherServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddServices()
            .AddMockedDbContext();

        public PublisherServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        // Tests using first option
        [Fact]
        public async Task GetPublishersAsync_ReturnsCorrectNumber()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDbContext.PrepareDataAsync(dbContext);

            var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

            // Act
            var result = await publisherService.GetPublishersAsync(null);
            var publisherDetails = result.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dbContext.Publishers.Count(), publisherDetails.Count);
        }

        [Fact]
        public async Task GetPublisherByIdAsync_ExistingId_ReturnsPublisher()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDbContext.PrepareDataAsync(dbContext);
            var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

            var publisherToGet = dbContext.Publishers.First();

            // Act
            var result = (await publisherService.GetPublisherByIdAsync(publisherToGet.Id)).Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publisherToGet.Name, result.Name);
        }
         [Fact]
        public async Task CreatePublisherAsync_ReturnsNewPublisher()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDbContext.PrepareDataAsync(dbContext);
            var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();
            
            var publisherCreate = new PublisherCreate
            {
                Name = "New Publisher"
            };

            // Act
            var result = await publisherService.CreatePublisherAsync(publisherCreate);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(dbContext.Publishers.FirstOrDefault(a => a.Name == publisherCreate.Name));
            Assert.Equal(publisherCreate.Name, result.Name);
        }

        [Fact]
        public async Task UpdatePublisherAsync_ReturnsUpdatedPublisher()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDbContext.PrepareDataAsync(dbContext);
            var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();
            
            var publisherId = 4; // Assuming publisher with Id 4 exists
            var publisherUpdate = new PublisherUpdate
            {
                Name = "Updated Publisher"
            };

            // Act
            var result = (await publisherService.UpdatePublisherAsync(publisherId, publisherUpdate)).Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publisherUpdate.Name, result.Name);
        }
        
        [Fact]
        public async Task DeletePublisherAsyncGetByIdDeleteAgain_ReturnsGenre()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDbContext.PrepareDataAsync(dbContext);
            var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();
            
            var publisherToDelete = dbContext.Genres.First();
            
            await publisherService.DeletePublisherAsync(publisherToDelete.Id);
        }
    }
}
