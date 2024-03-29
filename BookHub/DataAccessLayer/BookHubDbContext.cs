using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccessLayer;

public class BookHubDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<BookOrder> BookOrders { get; set; }
    public DbSet<User> Users { get; set; }


    public BookHubDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(LoggerFactory.Create(
                builder =>
                {
                    builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                                                           && level == LogLevel.Information).AddConsole();
                })).EnableSensitiveDataLogging();
        // .UseLazyLoadingProxies()
    }

    // https://docs.microsoft.com/en-us/ef/core/modeling/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(book => book.PrimaryGenre)
            .WithMany(genre => genre.PrimaryGenreBooks)
            .HasForeignKey(book => book.PrimaryGenreId);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Books)
            .WithMany(b => b.Orders)
            .UsingEntity<BookOrder>();

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}