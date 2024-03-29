using DataAccessLayer;
using EntityFrameworkCore.Testing.NSubstitute.Helpers;
using TestUtilities.Data;

namespace TestUtilities.MockedObjects
{
    public static class MockedDbContext
    {
        public static string RandomDbName => Guid.NewGuid().ToString();

        public static DbContextOptions<BookHubDbContext> GenerateNewInMemoryDbContextOptions()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BookHubDbContext>()
                .UseInMemoryDatabase(RandomDbName)
                .UseLazyLoadingProxies(false)
                .Options;

            return dbContextOptions;
        }

        public static BookHubDbContext CreateFromOptions(DbContextOptions<BookHubDbContext> options)
        {
            var dbContextToMock = new BookHubDbContext(options);

            var dbContext = new MockedDbContextBuilder<BookHubDbContext>()
                .UseDbContext(dbContextToMock)
                .UseConstructorWithParameters(options)
                .MockedDbContext;

            PrepareData(dbContext);

            return dbContext;
        }

        public static void PrepareData(BookHubDbContext dbContext)
        {
            dbContext.Orders.AddRange(TestData.GetMockedOrders());
            dbContext.Publishers.AddRange(TestData.GetMockedPublishers());
            dbContext.Authors.AddRange(TestData.GetMockedAuthors());
            dbContext.Genres.AddRange(TestData.GetMockedGenres());
            dbContext.Books.AddRange(TestData.GetMockedBooks());
            dbContext.Users.AddRange(TestData.GetMockedUsers());
            dbContext.Ratings.AddRange(TestData.GetMockedRatings());
            dbContext.SaveChanges();
        }

        public static async Task PrepareDataAsync(BookHubDbContext dbContext)
        {
            dbContext.Orders.AddRange(TestData.GetMockedOrders());
            dbContext.Publishers.AddRange(TestData.GetMockedPublishers());
            dbContext.Authors.AddRange(TestData.GetMockedAuthors());
            dbContext.Genres.AddRange(TestData.GetMockedGenres());
            dbContext.Books.AddRange(TestData.GetMockedBooks());
            dbContext.Users.AddRange(TestData.GetMockedUsers());
            dbContext.Ratings.AddRange(TestData.GetMockedRatings());
            await dbContext.SaveChangesAsync();
        }
    }
}