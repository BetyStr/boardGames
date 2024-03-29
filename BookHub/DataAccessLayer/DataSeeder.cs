using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public static class DataSeeder
{
    private static readonly PasswordHasher<User> Hasher = new PasswordHasher<User>();

    public static void Seed(this ModelBuilder modelBuilder)
    {
        Console.WriteLine("Seeding!");
        modelBuilder.Entity<Author>().HasData(PrepareAuthors());
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().HasData(PrepareUsers());
        modelBuilder.Entity<IdentityRole<int>>().HasData(PrepareRoles());
        modelBuilder.Entity<Publisher>().HasData(PreparePublishers());
        modelBuilder.Entity<Genre>().HasData(PrepareGenres());
        modelBuilder.Entity<Book>().HasData(PrepareBooks());
        modelBuilder.Entity<Rating>().HasData(PrepareRatings());
        modelBuilder.Entity<Order>().HasData(PrepareOrders());
        modelBuilder.Entity<BookOrder>().HasData(PrepareBookOrders());
        modelBuilder.Entity("AuthorBook").HasData(
            new { AuthorsId = 15, BooksId = 1 },
            new { AuthorsId = 14, BooksId = 10 },
            new { AuthorsId = 13, BooksId = 11 },
            new { AuthorsId = 13, BooksId = 12 },
            new { AuthorsId = 1, BooksId = 1 },
            new { AuthorsId = 1, BooksId = 2 },
            new { AuthorsId = 2, BooksId = 3 },
            new { AuthorsId = 2, BooksId = 4 },
            new { AuthorsId = 2, BooksId = 5 },
            new { AuthorsId = 3, BooksId = 5 },
            new { AuthorsId = 3, BooksId = 6 },
            new { AuthorsId = 3, BooksId = 7 },
            new { AuthorsId = 4, BooksId = 8 },
            new { AuthorsId = 4, BooksId = 9 },
            new { AuthorsId = 5, BooksId = 10 },
            new { AuthorsId = 5, BooksId = 11 },
            new { AuthorsId = 5, BooksId = 12 },
            new { AuthorsId = 5, BooksId = 13 },
            new { AuthorsId = 6, BooksId = 14 },
            new { AuthorsId = 6, BooksId = 15 },
            new { AuthorsId = 6, BooksId = 16 },
            new { AuthorsId = 7, BooksId = 17 },
            new { AuthorsId = 7, BooksId = 18 },
            new { AuthorsId = 7, BooksId = 19 },
            new { AuthorsId = 7, BooksId = 20 },
            new { AuthorsId = 7, BooksId = 21 },
            new { AuthorsId = 8, BooksId = 22 },
            new { AuthorsId = 8, BooksId = 23 },
            new { AuthorsId = 8, BooksId = 24 },
            new { AuthorsId = 9, BooksId = 25 },
            new { AuthorsId = 9, BooksId = 26 },
            new { AuthorsId = 9, BooksId = 27 },
            new { AuthorsId = 10, BooksId = 28 },
            new { AuthorsId = 11, BooksId = 29 },
            new { AuthorsId = 12, BooksId = 30 },
            new { AuthorsId = 13, BooksId = 31 },
            new { AuthorsId = 14, BooksId = 32 },
            new { AuthorsId = 14, BooksId = 33 },
            new { AuthorsId = 15, BooksId = 34 },
            new { AuthorsId = 15, BooksId = 35 });
        modelBuilder.Entity("BookGenre").HasData(
            new { BooksId = 1, GenresId = 1 },
            new { BooksId = 2, GenresId = 2 },
            new { BooksId = 3, GenresId = 3 },
            new { BooksId = 3, GenresId = 8 },
            new { BooksId = 3, GenresId = 13 },
            new { BooksId = 4, GenresId = 3 },
            new { BooksId = 5, GenresId = 4 },
            new { BooksId = 5, GenresId = 13 },
            new { BooksId = 6, GenresId = 5 },
            new { BooksId = 7, GenresId = 6 },
            new { BooksId = 7, GenresId = 7 },
            new { BooksId = 8, GenresId = 7 },
            new { BooksId = 9, GenresId = 8 },
            new { BooksId = 10, GenresId = 9 },
            new { BooksId = 11, GenresId = 10 },
            new { BooksId = 12, GenresId = 11 },
            new { BooksId = 13, GenresId = 12 },
            new { BooksId = 14, GenresId = 12 },
            new { BooksId = 15, GenresId = 12 },
            new { BooksId = 16, GenresId = 13 },
            new { BooksId = 17, GenresId = 13 },
            new { BooksId = 18, GenresId = 14 },
            new { BooksId = 19, GenresId = 15 },
            new { BooksId = 19, GenresId = 17 },
            new { BooksId = 19, GenresId = 1 },
            new { BooksId = 20, GenresId = 16 },
            new { BooksId = 20, GenresId = 2 },
            new { BooksId = 21, GenresId = 17 },
            new { BooksId = 22, GenresId = 17 },
            new { BooksId = 23, GenresId = 18 },
            new { BooksId = 23, GenresId = 3 },
            new { BooksId = 24, GenresId = 19 },
            new { BooksId = 25, GenresId = 20 },
            new { BooksId = 26, GenresId = 5 },
            new { BooksId = 27, GenresId = 6 },
            new { BooksId = 28, GenresId = 7 },
            new { BooksId = 29, GenresId = 13 },
            new { BooksId = 30, GenresId = 17 },
            new { BooksId = 31, GenresId = 16 },
            new { BooksId = 32, GenresId = 12 },
            new { BooksId = 33, GenresId = 9 },
            new { BooksId = 34, GenresId = 20 },
            new { BooksId = 35, GenresId = 10 },
            new { BooksId = 35, GenresId = 8 },
            new { BooksId = 35, GenresId = 5 });
        // Wishlist
        modelBuilder.Entity("BookUser").HasData(
            new { BooksId = 1, UsersId = 1 },
            new { BooksId = 2, UsersId = 2 },
            new { BooksId = 3, UsersId = 3 },
            new { BooksId = 3, UsersId = 8 },
            new { BooksId = 3, UsersId = 13 },
            new { BooksId = 4, UsersId = 3 },
            new { BooksId = 5, UsersId = 4 },
            new { BooksId = 5, UsersId = 13 },
            new { BooksId = 6, UsersId = 5 },
            new { BooksId = 7, UsersId = 6 },
            new { BooksId = 7, UsersId = 7 },
            new { BooksId = 8, UsersId = 7 },
            new { BooksId = 9, UsersId = 8 },
            new { BooksId = 10, UsersId = 9 },
            new { BooksId = 12, UsersId = 9 },
            new { BooksId = 11, UsersId = 10 },
            new { BooksId = 12, UsersId = 11 },
            new { BooksId = 13, UsersId = 12 },
            new { BooksId = 14, UsersId = 12 },
            new { BooksId = 15, UsersId = 12 },
            new { BooksId = 16, UsersId = 13 },
            new { BooksId = 17, UsersId = 13 },
            new { BooksId = 18, UsersId = 14 },
            new { BooksId = 19, UsersId = 15 },
            new { BooksId = 19, UsersId = 12 },
            new { BooksId = 19, UsersId = 1 },
            new { BooksId = 20, UsersId = 8 },
            new { BooksId = 20, UsersId = 2 },
            new { BooksId = 21, UsersId = 9 },
            new { BooksId = 22, UsersId = 10 },
            new { BooksId = 23, UsersId = 11 },
            new { BooksId = 23, UsersId = 3 },
            new { BooksId = 24, UsersId = 2 },
            new { BooksId = 25, UsersId = 1 },
            new { BooksId = 26, UsersId = 5 },
            new { BooksId = 27, UsersId = 6 },
            new { BooksId = 28, UsersId = 7 },
            new { BooksId = 29, UsersId = 13 },
            new { BooksId = 30, UsersId = 15 },
            new { BooksId = 31, UsersId = 7 },
            new { BooksId = 32, UsersId = 12 },
            new { BooksId = 33, UsersId = 9 },
            new { BooksId = 34, UsersId = 14 },
            new { BooksId = 35, UsersId = 3 },
            new { BooksId = 35, UsersId = 8 },
            new { BooksId = 35, UsersId = 5 });
        modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            new { UserId = 1, RoleId = 1 },
            new { UserId = 2, RoleId = 1 },
            new { UserId = 3, RoleId = 1 },
            new { UserId = 4, RoleId = 2 },
            new { UserId = 5, RoleId = 2 },
            new { UserId = 6, RoleId = 2 },
            new { UserId = 7, RoleId = 2 },
            new { UserId = 8, RoleId = 2 },
            new { UserId = 9, RoleId = 2 },
            new { UserId = 10, RoleId = 2 },
            new { UserId = 11, RoleId = 2 },
            new { UserId = 12, RoleId = 2 },
            new { UserId = 13, RoleId = 2 },
            new { UserId = 14, RoleId = 2 },
            new { UserId = 15, RoleId = 2 });
    }

    private static IEnumerable<Author> PrepareAuthors()
    {
        return new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "J. K. Rowling"
            },
            new Author
            {
                Id = 2,
                Name = "George R. R. Martin"
            },
            new Author
            {
                Id = 3,
                Name = "Stephen King"
            },
            new Author
            {
                Id = 4,
                Name = "Agatha Christie"
            },
            new Author
            {
                Id = 5,
                Name = "J.R.R. Tolkien"
            },
            new Author
            {
                Id = 6,
                Name = "Jane Austen"
            },
            new Author
            {
                Id = 7,
                Name = "Mark Twain"
            },
            new Author
            {
                Id = 8,
                Name = "Charles Dickens"
            },
            new Author
            {
                Id = 9,
                Name = "Harper Lee"
            },
            new Author
            {
                Id = 10,
                Name = "Leo Tolstoy"
            },
            new Author
            {
                Id = 11,
                Name = "Agnes Christie"
            },
            new Author
            {
                Id = 12,
                Name = "Ernest Hemingway"
            },
            new Author
            {
                Id = 13,
                Name = "Virginia Woolf"
            },
            new Author
            {
                Id = 14,
                Name = "Arthur Conan Doyle"
            },
            new Author
            {
                Id = 15,
                Name = "Antoine de Saint-Exupéry"
            }
        };
    }

    private static IEnumerable<Publisher> PreparePublishers()
    {
        return new List<Publisher>
        {
            new Publisher
            {
                Id = 1,
                Name = "Bloomsbury"
            },
            new Publisher
            {
                Id = 2,
                Name = "Secker & Warburg"
            },
            new Publisher
            {
                Id = 3,
                Name = "Reynal & Hitchcock"
            },
            new Publisher
            {
                Id = 4,
                Name = "Penguin Books"
            },
            new Publisher
            {
                Id = 5,
                Name = "Random House"
            },
            new Publisher
            {
                Id = 6,
                Name = "HarperCollins"
            },
            new Publisher
            {
                Id = 7,
                Name = "Simon & Schuster"
            },
            new Publisher
            {
                Id = 8,
                Name = "Macmillan Publishers"
            },
            new Publisher
            {
                Id = 9,
                Name = "Hachette Book Group"
            },
            new Publisher
            {
                Id = 10,
                Name = "Scholastic Corporation"
            },
            new Publisher
            {
                Id = 11,
                Name = "Oxford University Press"
            },
            new Publisher
            {
                Id = 12,
                Name = "Cambridge University Press"
            },
            new Publisher
            {
                Id = 13,
                Name = "Wiley"
            },
            new Publisher
            {
                Id = 14,
                Name = "Elsevier"
            },
            new Publisher
            {
                Id = 15,
                Name = "Springer"
            }
        };
    }

    private static IEnumerable<Genre> PrepareGenres()
    {
        return new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Name = "Dystopian"
            },
            new Genre
            {
                Id = 2,
                Name = "Mystery"
            },
            new Genre
            {
                Id = 3,
                Name = "Science Fiction"
            },
            new Genre
            {
                Id = 4,
                Name = "Romance"
            },
            new Genre
            {
                Id = 5,
                Name = "Fantasy"
            },
            new Genre
            {
                Id = 6,
                Name = "Thriller"
            },
            new Genre
            {
                Id = 7,
                Name = "Historical Fiction"
            },
            new Genre
            {
                Id = 8,
                Name = "Horror"
            },
            new Genre
            {
                Id = 9,
                Name = "Adventure"
            },
            new Genre
            {
                Id = 10,
                Name = "Biography"
            },
            new Genre
            {
                Id = 11,
                Name = "Non-fiction"
            },
            new Genre
            {
                Id = 12,
                Name = "Children's"
            },
            new Genre
            {
                Id = 13,
                Name = "Young Adult"
            },
            new Genre
            {
                Id = 14,
                Name = "Humor"
            },
            new Genre
            {
                Id = 15,
                Name = "Crime"
            },
            new Genre
            {
                Id = 16,
                Name = "Historical Romance"
            },
            new Genre
            {
                Id = 17,
                Name = "Western"
            },
            new Genre
            {
                Id = 18,
                Name = "Science Fantasy"
            },
            new Genre
            {
                Id = 19,
                Name = "Self-help"
            },
            new Genre
            {
                Id = 20,
                Name = "Cookbook"
            }
        };
    }

    private static IEnumerable<Book> PrepareBooks()
    {
        var books = new List<Book>();
        var random = new Random();
        var names = new List<string>
        {
            "To Kill a Mockingbird",
            "1984",
            "James Bond",
            "The Great Gatsby",
            "One Hundred Years of Solitude",
            "The Catcher in the Rye",
            "Brave New World",
            "The Hobbit",
            "Love and Basketball",
            "Pride and Prejudice",
            "The Lord of the Rings: The Fellowship of the Ring",
            "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe",
            "Harry Potter and the Philosopher's Stone",
            "The Hunger Games",
            "The Da Vinci Code",
            "A Game of Thrones",
            "The Shining",
            "The Hitchhiker's Guide to the Galaxy",
            "The Alchemist",
            "War and Peace",
            "Crime and Punishment",
            "The Catch-22",
            "The Grapes of Wrath",
            "Fahrenheit 451",
            "Lord of the Flies",
            "Moby-Dick",
            "Frankenstein",
            "Alice's Adventures in Wonderland",
            "Dracula",
            "The Odyssey",
            "Romeo and Juliet",
            "Hamlet",
            "Macbeth",
            "Othello",
            "The Divine Comedy",
            "Don Quixote"
        };

        for (var i = 0; i < names.Count; i++)
        {
            var randomPrice = (decimal)Math.Round(random.NextDouble() * (25 - 5) + 5, 2);
            books.Add(new Book
            {
                Id = i + 1,
                Name = names[i],
                PublisherId = random.Next(1, 15),
                PrimaryGenreId = random.Next(1, 20),
                StockInStorage = random.Next(1, 50),
                Price = randomPrice,
                OverallRating = random.Next(30, 100)
            });
        }

        return books;
    }

    private static IEnumerable<User> PrepareUsers()
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
            var name = names[i].Substring(0, names[i].IndexOf(' ')).ToLower();
            var email = $"{name}@gmail.com";
            users.Add(new User
            {
                Id = i + 1,
                UserName = name,
                NormalizedUserName = name.ToUpper(),
                Name = names[i],
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = Hasher.HashPassword(null, "Aa123!")
            });
        }

        return users;
    }

    private static IEnumerable<Order> PrepareOrders()
    {
        var orderStatuses = new List<OrderStatus>
        {
            OrderStatus.Unpaid, OrderStatus.Paid, OrderStatus.AwaitingShipment, OrderStatus.Shipped,
            OrderStatus.Delivered
        };
        var random = new Random();
        var orders = new List<Order>();
        for (var i = 1; i <= 149; i++)
        {
            var randomPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2);
            orders.Add(new Order
            {
                Id = i,
                UserId = i / 10 + 1,
                TotalPrice = randomPrice,
                OrderStatus = orderStatuses[random.Next(1, 5)]
            });
        }

        return orders;
    }

    private static IEnumerable<BookOrder> PrepareBookOrders()
    {
        var bookOrders = new List<BookOrder>();
        var random = new Random();
        for (var i = 1; i <= 149; i++)
        {
            for (var j = 0; j <= random.Next(1, 5); j++)
            {
                bookOrders.Add(new BookOrder
                {
                    OrderId = i,
                    BookId = j == 0 ? j + 1 : j * 5,
                    Count = random.Next(1, 3)
                });
            }
        }

        return bookOrders;
    }

    private static IEnumerable<IdentityRole<int>> PrepareRoles()
    {
        return new List<IdentityRole<int>>
        {
            new()
            {
                Id = 1,
                Name = UserRoles.Admin,
                NormalizedName = UserRoles.Admin.ToUpper(),
            },
            new()
            {
                Id = 2,
                Name = UserRoles.User,
                NormalizedName = UserRoles.User.ToUpper(),
            }
        };
    }

    private static IEnumerable<Rating> PrepareRatings()
    {
        var random = new Random();
        var ratings = new List<Rating>();
        var comments = new List<string>
        {
            "Great book but it gave me an existential crisis bigger than I had before",
            "Couldn't put it down, finished it in one sitting!",
            "The plot twists in this book are mind-blowing",
            "A classic that everyone should read",
            "The characters are so well-developed, felt like they were real people",
            "This book made me laugh and cry, a roller coaster of emotions",
            "The writing style is beautiful, every sentence is a work of art",
            "I couldn't guess the ending, kept me guessing until the last page",
            "I wish there was a sequel, I'm not ready to say goodbye to these characters",
            "The themes explored in this book are thought-provoking",
            "The pacing is perfect, kept me engaged from start to finish",
            "This book challenged my perspective on life",
            "The world-building is exceptional, I felt like I was there",
            "A must-read for book lovers",
            "The author's storytelling is captivating",
            "This book is a page-turner, couldn't stop reading",
            "The dialogue between characters is witty and realistic",
            "I've recommended this book to all my friends",
            "It left me with a book hangover, couldn't stop thinking about it",
            "",
            "Couldn't get into the story, found it boring from the start",
            "The characters felt one-dimensional and uninteresting",
            "The plot was predictable, I expected more twists",
            "I didn't connect with the protagonist, lacked depth",
            "The writing style was confusing and hard to follow",
            "This book didn't live up to the hype, very disappointing",
            "The ending felt rushed and unresolved",
            "Too much exposition, not enough action",
            "I found the dialogue unrealistic and forced",
            "The author tried too hard to be profound, came off as pretentious",
            "The pacing was off, some parts dragged on while others felt rushed",
            "The world-building was weak and inconsistent",
            "I couldn't sympathize with any of the characters",
            "The themes explored were cliché and overdone",
            "The book didn't live up to the reviews, a letdown",
            "The grammar and editing were poor, distracting from the story",
            "The book felt like a rip-off of [another popular book]",
            "The author relied too heavily on stereotypes",
            "I regret spending time on this book, wish I chose something else",
            "The climax was anticlimactic, left me unsatisfied"
        };
        for (var i = 0; i < 50; i++)
        {
            ratings.Add(new Rating
            {
                Id = i + 1,
                UserId = random.Next(1,
                    15),
                BookId = random.Next(1,
                    35),
                Value = random.Next(10,
                    100),
                Comment = comments[i % comments.Count]
            });
        }

        return ratings;
    }
}