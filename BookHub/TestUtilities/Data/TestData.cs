using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace TestUtilities.Data;

public static class TestData
{
    private static readonly PasswordHasher<User> Hasher = new();
    
    public static IEnumerable<Publisher> GetMockedPublishers()
    {
        return new List<Publisher>
        {
            new Publisher
            {
                Id = 1,
                Name = "Bloomsbury",
            },
            new Publisher
            {
                Id = 2,
                Name = "Secker & Warburg",
            },
            new Publisher
            {
                Id = 3,
                Name = "Reynal & Hitchcock",
            },
            new Publisher
            {
                Id = 4,
                Name = "Penguin Books",

            },
            new Publisher
            {
                Id = 5,
                Name = "Random House",
            }
        };
    }

    public static IEnumerable<Order> GetMockedOrders()
    {
        var random = new Random();

        return new List<Order>
        {
            new Order
            {
                Id = 1,
                UserId = 1,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                Date = new DateTime(2023, 3, 10),
            },
            new Order
            {
                Id = 2,
                UserId = 2,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                Date = new DateTime(2023, 1, 24),
            },
            new Order
            {
                Id = 3,
                UserId = 3,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                Date = new DateTime(2023, 7, 5),
            },
            new Order
            {
                Id = 4,
                UserId = 4,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                Date = new DateTime(2023, 12, 1),
            },
            new Order
            {
                Id = 5,
                UserId = 5,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                Date = new DateTime(2023, 4, 11),
            }
        };
    }
    
    public static IEnumerable<Author> GetMockedAuthors()
    {
        return new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "J. K. Rowling",
            },
            new Author
            {
                Id = 2,
                Name = "George R. R. Martin",
            },
            new Author
            {
                Id = 3,
                Name = "Stephen King",
            },
            new Author
            {
                Id = 4,
                Name = "Agatha Christie",
            },
            new Author
            {
                Id = 5,
                Name = "J.R.R. Tolkien",
            }
        };
    }

    public static IEnumerable<Genre> GetMockedGenres()
    {
        return new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Name = "Dystopian",
            },
            new Genre
            {
                Id = 2,
                Name = "Mystery",
            },
            new Genre
            {
                Id = 3,
                Name = "Science Fiction",
            },
            new Genre
            {
                Id = 4,
                Name = "Romance",
            },
            new Genre
            {
                Id = 5,
                Name = "Fantasy",
            }
        };
    }

    public static IEnumerable<Book> GetMockedBooks()
    {
        var random = new Random();
        return new List<Book>
        {
            new Book
            {
                Id = 1,
                Name = "To Kill a Mockingbird",
                PublisherId = 1,
                PrimaryGenreId = 1,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
                Authors = new List<Author>()
            },
            new Book
            {
                Id = 2,
                Name = "Harry Potter",
                PublisherId = 2,
                PrimaryGenreId = 2,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
                Authors = new List<Author>()
            },
            new Book
            {
                Id = 3,
                Name = "Narnia",
                PublisherId = 3,
                PrimaryGenreId = 3,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
            },
            new Book
            {
                Id = 4,
                Name = "Pride and Prejudice",
                PublisherId = 4,
                PrimaryGenreId = 4,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
            },
            new Book
            {
                Id = 5,
                Name = "Eragon",
                PublisherId = 5,
                PrimaryGenreId = 5,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
            },
        };
    }

    public static IEnumerable<User> GetMockedUsers()
    {
        var users = new List<User>();
        var names = new List<string>
        {
            "Roman Mario",
            "Beth Story",
            "Monika Reha",
            "John Smith",
            "James Bond",
            "Filip Strong",
            "Random Guy",
            "Jack Black",
            "Tom Smart",
            "Ali Willy",
            "Rubber Duck",
            "Olaf Snow",
            "Good Programmer",
            "Tim King",
            "Adam Queen"
        };
        for (var i = 0; i < names.Count; i++)
        {
            var name = names[i][..names[i].IndexOf(' ')];
            var email = $"{name}@gmail.com";
            users.Add(new User
            {
                Id = i + 1,
                UserName = name.ToLower(),
                NormalizedUserName = name.ToUpper(),
                Name = names[i],
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = Hasher.HashPassword(null, "Aa123!"),
            });
        }
        return users;
    }

    public static IEnumerable<Rating> GetMockedRatings()
    {
        var random = new Random();
        var ratings = new List<Rating>();
        var comments = new List<string>
        {
            "Great book but it gave me an existential crisis bigger than I had before",
            "Couldn't put it down, finished it in one sitting!",
            "The plot twists in this book are mind-blowing",
            "A classic that everyone should read",
            "The characters are so well-developed, felt like they were real people"
        };
        for (var i = 0; i < 5; i++)
        {
            ratings.Add(new Rating
            {
                Id = i + 1,
                UserId = i % 2 + 1,
                BookId = i % 2 + 1,
                Value = random.Next(10, 100),
                Comment = comments[i],
            });
        }
        return ratings;
    }
}