using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Postgres.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PublisherId = table.Column<int>(type: "integer", nullable: false),
                    PrimaryGenreId = table.Column<int>(type: "integer", nullable: false),
                    StockInStorage = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    OverallRating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genres_PrimaryGenreId",
                        column: x => x.PrimaryGenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "integer", nullable: false),
                    BooksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "integer", nullable: false),
                    GenresId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => new { x.BooksId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_BookGenre_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    BuyUnitPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrders", x => new { x.BookId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_BookOrders_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookUser",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookUser", x => new { x.BooksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BookUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookUser_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "a667a608-c0ec-45b6-b84f-12c8e7175113", "roman@gmail.com", true, false, null, "Roman Mario", "ROMAN@GMAIL.COM", "ROMAN", "AQAAAAIAAYagAAAAEKpCKi1mLnYoNHU9u3Tih5dUNJ9CKrR+oscTSWLuU+3WeNFJxmEk2yndgVzum/4AQw==", null, false, "e6b88210-251b-4994-ab4a-f3b1ed9a737b", false, "roman" },
                    { 2, 0, "3691c352-4e9d-45c5-b7f9-71f3fcfff56b", "beth@gmail.com", true, false, null, "Beth Story", "BETH@GMAIL.COM", "BETH", "AQAAAAIAAYagAAAAEDrLqDKTZza6aX1HY/V1ZjlTRrjqi0wQCqg05GRIot9xPBGhxC1+dA8CYDUnEgOGGA==", null, false, "92de9dbe-ba7d-4a28-8105-8724d69c11b3", false, "beth" },
                    { 3, 0, "6445debd-8b4f-43a4-aff1-169fb3f81fa6", "monika@gmail.com", true, false, null, "Monika Reha", "MONIKA@GMAIL.COM", "MONIKA", "AQAAAAIAAYagAAAAEHiUH/sESATUvNUhw5crAjWjfaFfj1cwLx0EsQ4yXWmTHzEuhynQVBj+oqpKEuongw==", null, false, "e90c56a4-8cb2-4fd2-a265-0d2aa1a09f63", false, "monika" },
                    { 4, 0, "3178ee95-a637-4177-a991-3b129ea3d4c2", "john@gmail.com", true, false, null, "John Smith", "JOHN@GMAIL.COM", "JOHN", "AQAAAAIAAYagAAAAEOY2aTZuQEzEYEAKMY/u5UihsCuLTuutsGqgHbRuUdc/7h9LkGUg9cMHH5hrs5HxXg==", null, false, "65c408c0-f380-4733-97ba-82a016f3d81f", false, "john" },
                    { 5, 0, "3f33205f-90fa-412e-b3a9-c59b2d7e4bc6", "james@gmail.com", true, false, null, "James Bond", "JAMES@GMAIL.COM", "JAMES", "AQAAAAIAAYagAAAAEJXszuYhciVVNSffnGC65QCrzY/BvBY4c1jA2IAUyYPUh63vkbZxaYNgO89hOUUJ7Q==", null, false, "9d1e2254-1c23-440c-8a13-22386ac32229", false, "james" },
                    { 6, 0, "daa5d247-a506-45e4-8ec4-359b3d0aaab5", "filip@gmail.com", true, false, null, "Filip Strong", "FILIP@GMAIL.COM", "FILIP", "AQAAAAIAAYagAAAAECJjXeYxlRZgofX0dUaX0H6dQNeGuoHdmlGGhUDn8u/Fb9xAaGFaW7r86d6S00v25g==", null, false, "7cc02390-684f-4534-b134-8ba0b06abf84", false, "filip" },
                    { 7, 0, "dc8fa8b8-76ae-4442-9621-f957bd7e1dd2", "random@gmail.com", true, false, null, "Random Guy", "RANDOM@GMAIL.COM", "RANDOM", "AQAAAAIAAYagAAAAED6RGQvgDEADUlXBrFddmPOpBqhAOGNskSp479kiz/h0SHI6CpRzznKANkRqlMMefg==", null, false, "f345c711-0d2f-4b56-8edd-d55671fb6bf4", false, "random" },
                    { 8, 0, "68fb29ac-2108-4187-8482-ccebcfbea653", "jack@gmail.com", true, false, null, "Jack Black", "JACK@GMAIL.COM", "JACK", "AQAAAAIAAYagAAAAEL3UvliWxzSjMMx2eprYn0hAgDILV83BbX0Cfwh6UHIXqUVf9zAR5W9tzb8NFw6xYw==", null, false, "9892bfe5-b766-46bb-9575-1868ee5eba3c", false, "jack" },
                    { 9, 0, "b2ea243e-3411-40b6-b49a-266627b95c42", "tom@gmail.com", true, false, null, "Tom Smart", "TOM@GMAIL.COM", "TOM", "AQAAAAIAAYagAAAAENaPuXmU/CPtvkbzB5Eb2VN0EMXwei4+yIslJg2qsfIzAg5KmrY/saTFDPvHuuuShw==", null, false, "d2ee430b-30fb-4190-8315-3f8b7805b0f4", false, "tom" },
                    { 10, 0, "fb1ed7a5-4c5f-4cb2-ab8c-9f5fe67252f7", "ali@gmail.com", true, false, null, "Ali Willy", "ALI@GMAIL.COM", "ALI", "AQAAAAIAAYagAAAAEFbtxxW4NnB9OG7lg4/btQc/9IqNyIg3rk34NQZPFtr7XHItUHB1Vd3M0VICdBFzFQ==", null, false, "3c53f22b-a6b9-42bc-8301-4e936542f10b", false, "ali" },
                    { 11, 0, "c62831ab-ddc5-4e85-96de-15ac39efb0f0", "rubber@gmail.com", true, false, null, "Rubber Duck", "RUBBER@GMAIL.COM", "RUBBER", "AQAAAAIAAYagAAAAEDyIA29FFlXYx+EYN5a97kxSFcQHgWlnE6UnBU/D6CSDKgU7M2jawLHcgAz6/nVxUw==", null, false, "8c4b26cb-8270-47f3-bab5-6af347ca7954", false, "rubber" },
                    { 12, 0, "0855f993-f2ee-458b-a389-fed7d9cd09f7", "olaf@gmail.com", true, false, null, "Olaf Snow", "OLAF@GMAIL.COM", "OLAF", "AQAAAAIAAYagAAAAEPQehI/9zWy+RtnLrpYLaF8RfWf7Vof0QgcfRnHNMb+dW6SkZnh22xGqLxIk4rGCKA==", null, false, "20e38720-7a00-4b77-95e4-e63cc574c37c", false, "olaf" },
                    { 13, 0, "59a1d2da-de20-45ab-bc1b-069e6d4314d9", "good@gmail.com", true, false, null, "Good Programmer", "GOOD@GMAIL.COM", "GOOD", "AQAAAAIAAYagAAAAEAYkYJ9BmW7jXoszWCkGfeKJlzFE5ZJllMK7hZMiDo8snyz4oZBb8aL+vIRuQQdg2Q==", null, false, "359c245a-3c24-480c-adc0-6712cf961c14", false, "good" },
                    { 14, 0, "5e473e11-41ae-4a7d-97f6-2b4c0fbfbaba", "tim@gmail.com", true, false, null, "Tim King", "TIM@GMAIL.COM", "TIM", "AQAAAAIAAYagAAAAEBZpxxhtuWlMsOnzRmJe+d3CD1UrJxPVwQIP9AF9NVOLpH3ZN15NeWrefMxwiEjpcQ==", null, false, "d5a5be38-4c2c-4578-8128-b101152a35cb", false, "tim" },
                    { 15, 0, "ca9793c1-e885-4c94-a7aa-4abaf24db7d4", "adam@gmail.com", true, false, null, "Adam Queen", "ADAM@GMAIL.COM", "ADAM", "AQAAAAIAAYagAAAAEGGLraQ39Ti80G5RT4fPGWGmx5+jlYfrdhPvy6It6u1civVrzqtky9vMrdRwPMbOgQ==", null, false, "f1dee71d-0b10-4872-afd6-a29660d1fd1a", false, "adam" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "J. K. Rowling" },
                    { 2, "George R. R. Martin" },
                    { 3, "Stephen King" },
                    { 4, "Agatha Christie" },
                    { 5, "J.R.R. Tolkien" },
                    { 6, "Jane Austen" },
                    { 7, "Mark Twain" },
                    { 8, "Charles Dickens" },
                    { 9, "Harper Lee" },
                    { 10, "Leo Tolstoy" },
                    { 11, "Agnes Christie" },
                    { 12, "Ernest Hemingway" },
                    { 13, "Virginia Woolf" },
                    { 14, "Arthur Conan Doyle" },
                    { 15, "Antoine de Saint-Exupéry" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dystopian" },
                    { 2, "Mystery" },
                    { 3, "Science Fiction" },
                    { 4, "Romance" },
                    { 5, "Fantasy" },
                    { 6, "Thriller" },
                    { 7, "Historical Fiction" },
                    { 8, "Horror" },
                    { 9, "Adventure" },
                    { 10, "Biography" },
                    { 11, "Non-fiction" },
                    { 12, "Children's" },
                    { 13, "Young Adult" },
                    { 14, "Humor" },
                    { 15, "Crime" },
                    { 16, "Historical Romance" },
                    { 17, "Western" },
                    { 18, "Science Fantasy" },
                    { 19, "Self-help" },
                    { 20, "Cookbook" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bloomsbury" },
                    { 2, "Secker & Warburg" },
                    { 3, "Reynal & Hitchcock" },
                    { 4, "Penguin Books" },
                    { 5, "Random House" },
                    { 6, "HarperCollins" },
                    { 7, "Simon & Schuster" },
                    { 8, "Macmillan Publishers" },
                    { 9, "Hachette Book Group" },
                    { 10, "Scholastic Corporation" },
                    { 11, "Oxford University Press" },
                    { 12, "Cambridge University Press" },
                    { 13, "Wiley" },
                    { 14, "Elsevier" },
                    { 15, "Springer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 },
                    { 2, 11 },
                    { 2, 12 },
                    { 2, 13 },
                    { 2, 14 },
                    { 2, 15 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Name", "OverallRating", "Price", "PrimaryGenreId", "PublisherId", "StockInStorage" },
                values: new object[,]
                {
                    { 1, "To Kill a Mockingbird", 96, 24.05m, 9, 8, 28 },
                    { 2, "1984", 47, 8.2m, 7, 5, 22 },
                    { 3, "James Bond", 68, 22.44m, 12, 12, 29 },
                    { 4, "The Great Gatsby", 34, 16.91m, 6, 2, 25 },
                    { 5, "One Hundred Years of Solitude", 56, 11.15m, 15, 9, 17 },
                    { 6, "The Catcher in the Rye", 96, 19.25m, 16, 3, 16 },
                    { 7, "Brave New World", 92, 5.25m, 16, 6, 15 },
                    { 8, "The Hobbit", 61, 7.26m, 6, 5, 16 },
                    { 9, "Love and Basketball", 44, 9.22m, 14, 13, 9 },
                    { 10, "Pride and Prejudice", 42, 5.57m, 10, 9, 18 },
                    { 11, "The Lord of the Rings: The Fellowship of the Ring", 47, 24.15m, 5, 10, 29 },
                    { 12, "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe", 30, 9.5m, 3, 8, 20 },
                    { 13, "Harry Potter and the Philosopher's Stone", 79, 17.94m, 15, 14, 44 },
                    { 14, "The Hunger Games", 34, 15.21m, 14, 6, 8 },
                    { 15, "The Da Vinci Code", 44, 17.05m, 5, 4, 35 },
                    { 16, "A Game of Thrones", 89, 13.45m, 7, 5, 17 },
                    { 17, "The Shining", 80, 15.55m, 19, 9, 40 },
                    { 18, "The Hitchhiker's Guide to the Galaxy", 40, 18.6m, 1, 7, 20 },
                    { 19, "The Alchemist", 85, 24.89m, 16, 5, 29 },
                    { 20, "War and Peace", 31, 20.02m, 16, 3, 25 },
                    { 21, "Crime and Punishment", 75, 14.48m, 5, 9, 3 },
                    { 22, "The Catch-22", 33, 12.3m, 5, 1, 6 },
                    { 23, "The Grapes of Wrath", 71, 21.42m, 5, 13, 26 },
                    { 24, "Fahrenheit 451", 49, 19.4m, 8, 9, 23 },
                    { 25, "Lord of the Flies", 71, 16.92m, 17, 5, 21 },
                    { 26, "Moby-Dick", 47, 10.33m, 13, 2, 14 },
                    { 27, "Frankenstein", 31, 12.4m, 4, 5, 7 },
                    { 28, "Alice's Adventures in Wonderland", 46, 16.73m, 14, 12, 7 },
                    { 29, "Dracula", 88, 18.02m, 1, 9, 14 },
                    { 30, "The Odyssey", 58, 15.46m, 18, 3, 46 },
                    { 31, "Romeo and Juliet", 65, 7.51m, 13, 12, 31 },
                    { 32, "Hamlet", 94, 15.09m, 5, 12, 12 },
                    { 33, "Macbeth", 68, 18.97m, 16, 3, 30 },
                    { 34, "Othello", 75, 12.78m, 7, 10, 27 },
                    { 35, "The Divine Comedy", 97, 12.95m, 8, 2, 32 },
                    { 36, "Don Quixote", 80, 5.46m, 10, 11, 18 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "OrderStatus", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4112), 1, 6.28m, 1 },
                    { 2, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4242), 2, 23.28m, 1 },
                    { 3, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4246), 3, 38.69m, 1 },
                    { 4, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4249), 2, 39.97m, 1 },
                    { 5, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4257), 2, 36.54m, 1 },
                    { 6, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4261), 1, 48.89m, 1 },
                    { 7, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4263), 4, 32.52m, 1 },
                    { 8, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4266), 1, 38.55m, 1 },
                    { 9, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4268), 3, 42.46m, 1 },
                    { 10, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4270), 3, 21.5m, 2 },
                    { 11, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4272), 3, 53.35m, 2 },
                    { 12, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4274), 1, 43.64m, 2 },
                    { 13, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4276), 4, 7.99m, 2 },
                    { 14, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4279), 2, 16.55m, 2 },
                    { 15, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4280), 1, 26.54m, 2 },
                    { 16, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4298), 2, 14.12m, 2 },
                    { 17, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4300), 2, 16.69m, 2 },
                    { 18, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4302), 2, 37.32m, 2 },
                    { 19, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4304), 4, 33.85m, 2 },
                    { 20, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4306), 2, 46.04m, 3 },
                    { 21, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4308), 2, 32.56m, 3 },
                    { 22, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4310), 2, 49.94m, 3 },
                    { 23, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4312), 1, 32.66m, 3 },
                    { 24, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4314), 2, 17.98m, 3 },
                    { 25, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4316), 3, 9.58m, 3 },
                    { 26, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4318), 4, 15.23m, 3 },
                    { 27, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4320), 4, 49.82m, 3 },
                    { 28, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4322), 3, 27.45m, 3 },
                    { 29, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4324), 1, 51.62m, 3 },
                    { 30, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4332), 3, 36.37m, 4 },
                    { 31, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4378), 3, 16.41m, 4 },
                    { 32, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4381), 1, 11.94m, 4 },
                    { 33, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4383), 4, 42.4m, 4 },
                    { 34, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4385), 1, 31.53m, 4 },
                    { 35, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4388), 2, 28.02m, 4 },
                    { 36, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4389), 2, 40.58m, 4 },
                    { 37, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4391), 3, 44.68m, 4 },
                    { 38, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4393), 3, 6.41m, 4 },
                    { 39, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4395), 1, 10.62m, 4 },
                    { 40, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4397), 4, 16.86m, 5 },
                    { 41, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4399), 4, 37.09m, 5 },
                    { 42, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4521), 4, 18.2m, 5 },
                    { 43, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4524), 1, 51.33m, 5 },
                    { 44, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4526), 1, 51.35m, 5 },
                    { 45, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4528), 2, 31.93m, 5 },
                    { 46, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4530), 2, 39.09m, 5 },
                    { 47, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4532), 4, 6.4m, 5 },
                    { 48, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4534), 3, 9.71m, 5 },
                    { 49, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4536), 1, 13.23m, 5 },
                    { 50, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4538), 4, 38.25m, 6 },
                    { 51, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4540), 3, 29.48m, 6 },
                    { 52, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4542), 4, 38.34m, 6 },
                    { 53, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4544), 4, 16.7m, 6 },
                    { 54, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4546), 3, 32.17m, 6 },
                    { 55, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4548), 1, 16.78m, 6 },
                    { 56, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4550), 1, 23.94m, 6 },
                    { 57, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4552), 1, 44.85m, 6 },
                    { 58, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4554), 3, 30.3m, 6 },
                    { 59, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4556), 1, 12.02m, 6 },
                    { 60, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4558), 4, 31.56m, 7 },
                    { 61, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4560), 4, 36.86m, 7 },
                    { 62, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4562), 1, 18.85m, 7 },
                    { 63, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4564), 3, 20.97m, 7 },
                    { 64, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4566), 3, 30.77m, 7 },
                    { 65, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4568), 1, 33.14m, 7 },
                    { 66, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4570), 3, 24.65m, 7 },
                    { 67, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4573), 2, 28.6m, 7 },
                    { 68, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4575), 3, 17.2m, 7 },
                    { 69, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4576), 2, 16.29m, 7 },
                    { 70, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4578), 2, 41.21m, 8 },
                    { 71, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4580), 3, 7.26m, 8 },
                    { 72, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4582), 2, 20.67m, 8 },
                    { 73, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4584), 2, 20.86m, 8 },
                    { 74, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4586), 1, 24.84m, 8 },
                    { 75, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4588), 2, 52.88m, 8 },
                    { 76, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4590), 1, 39.32m, 8 },
                    { 77, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4592), 2, 44.94m, 8 },
                    { 78, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4594), 1, 28.76m, 8 },
                    { 79, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4596), 3, 42.94m, 8 },
                    { 80, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4598), 1, 46.44m, 9 },
                    { 81, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4600), 2, 7.27m, 9 },
                    { 82, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4602), 3, 26.02m, 9 },
                    { 83, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4603), 1, 39.02m, 9 },
                    { 84, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4605), 2, 24.85m, 9 },
                    { 85, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4607), 1, 34.26m, 9 },
                    { 86, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4609), 3, 37.28m, 9 },
                    { 87, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4611), 1, 18.69m, 9 },
                    { 88, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4613), 3, 37.31m, 9 },
                    { 89, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4615), 1, 11.23m, 9 },
                    { 90, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4617), 4, 51.19m, 10 },
                    { 91, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4619), 4, 42.08m, 10 },
                    { 92, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4853), 3, 47.83m, 10 },
                    { 93, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4856), 1, 5.22m, 10 },
                    { 94, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4858), 1, 48.38m, 10 },
                    { 95, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4860), 3, 10.08m, 10 },
                    { 96, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4862), 4, 11.81m, 10 },
                    { 97, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4864), 2, 30.07m, 10 },
                    { 98, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4866), 2, 48.15m, 10 },
                    { 99, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4868), 3, 44.96m, 10 },
                    { 100, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4870), 3, 27.17m, 11 },
                    { 101, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4872), 4, 11.43m, 11 },
                    { 102, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4873), 1, 20.45m, 11 },
                    { 103, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4875), 2, 31.29m, 11 },
                    { 104, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4877), 4, 41.5m, 11 },
                    { 105, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4879), 4, 37.92m, 11 },
                    { 106, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4881), 1, 15.84m, 11 },
                    { 107, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4883), 1, 27.05m, 11 },
                    { 108, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4885), 3, 11.93m, 11 },
                    { 109, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4887), 3, 49.78m, 11 },
                    { 110, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4889), 3, 13.9m, 12 },
                    { 111, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4891), 1, 46.19m, 12 },
                    { 112, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4893), 1, 47.92m, 12 },
                    { 113, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4894), 4, 46.77m, 12 },
                    { 114, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4896), 1, 49.94m, 12 },
                    { 115, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4898), 3, 9.9m, 12 },
                    { 116, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4900), 2, 39.47m, 12 },
                    { 117, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4902), 2, 36.57m, 12 },
                    { 118, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4904), 3, 34.57m, 12 },
                    { 119, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4906), 1, 36.08m, 12 },
                    { 120, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4908), 4, 26.26m, 13 },
                    { 121, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4910), 2, 31.98m, 13 },
                    { 122, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4912), 1, 13.69m, 13 },
                    { 123, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4914), 2, 50.43m, 13 },
                    { 124, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4916), 2, 26.83m, 13 },
                    { 125, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4918), 2, 9.47m, 13 },
                    { 126, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4919), 2, 44.22m, 13 },
                    { 127, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4922), 3, 15m, 13 },
                    { 128, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4924), 1, 5.78m, 13 },
                    { 129, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4926), 4, 8.94m, 13 },
                    { 130, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4929), 4, 37.55m, 14 },
                    { 131, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4931), 4, 41.14m, 14 },
                    { 132, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4933), 1, 10.21m, 14 },
                    { 133, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4934), 3, 20.72m, 14 },
                    { 134, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4969), 2, 15.48m, 14 },
                    { 135, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4971), 1, 42.82m, 14 },
                    { 136, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4973), 3, 53.21m, 14 },
                    { 137, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4975), 2, 19.74m, 14 },
                    { 138, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4977), 2, 49.08m, 14 },
                    { 139, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4979), 1, 17.24m, 14 },
                    { 140, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4981), 2, 41.31m, 15 },
                    { 141, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4983), 1, 15.15m, 15 },
                    { 142, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4985), 2, 28.84m, 15 },
                    { 143, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4987), 1, 29.1m, 15 },
                    { 144, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4989), 1, 49.32m, 15 },
                    { 145, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4991), 3, 35.2m, 15 },
                    { 146, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4993), 4, 19.74m, 15 },
                    { 147, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4994), 3, 32.38m, 15 },
                    { 148, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4996), 3, 33.51m, 15 },
                    { 149, new DateTime(2024, 1, 14, 21, 36, 24, 913, DateTimeKind.Local).AddTicks(4998), 1, 40.12m, 15 }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 4, 8 },
                    { 4, 9 },
                    { 5, 10 },
                    { 5, 11 },
                    { 5, 12 },
                    { 5, 13 },
                    { 6, 14 },
                    { 6, 15 },
                    { 6, 16 },
                    { 7, 17 },
                    { 7, 18 },
                    { 7, 19 },
                    { 7, 20 },
                    { 7, 21 },
                    { 8, 22 },
                    { 8, 23 },
                    { 8, 24 },
                    { 9, 25 },
                    { 9, 26 },
                    { 9, 27 },
                    { 10, 28 },
                    { 11, 29 },
                    { 12, 30 },
                    { 13, 11 },
                    { 13, 12 },
                    { 13, 31 },
                    { 14, 10 },
                    { 14, 32 },
                    { 14, 33 },
                    { 15, 1 },
                    { 15, 34 },
                    { 15, 35 }
                });

            migrationBuilder.InsertData(
                table: "BookGenre",
                columns: new[] { "BooksId", "GenresId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 8 },
                    { 3, 13 },
                    { 4, 3 },
                    { 5, 4 },
                    { 5, 13 },
                    { 6, 5 },
                    { 7, 6 },
                    { 7, 7 },
                    { 8, 7 },
                    { 9, 8 },
                    { 10, 9 },
                    { 11, 10 },
                    { 12, 11 },
                    { 13, 12 },
                    { 14, 12 },
                    { 15, 12 },
                    { 16, 13 },
                    { 17, 13 },
                    { 18, 14 },
                    { 19, 1 },
                    { 19, 15 },
                    { 19, 17 },
                    { 20, 2 },
                    { 20, 16 },
                    { 21, 17 },
                    { 22, 17 },
                    { 23, 3 },
                    { 23, 18 },
                    { 24, 19 },
                    { 25, 20 },
                    { 26, 5 },
                    { 27, 6 },
                    { 28, 7 },
                    { 29, 13 },
                    { 30, 17 },
                    { 31, 16 },
                    { 32, 12 },
                    { 33, 9 },
                    { 34, 20 },
                    { 35, 5 },
                    { 35, 8 },
                    { 35, 10 }
                });

            migrationBuilder.InsertData(
                table: "BookOrders",
                columns: new[] { "BookId", "OrderId", "BuyUnitPrice", "Count" },
                values: new object[,]
                {
                    { 1, 1, 0m, 2 },
                    { 1, 2, 0m, 2 },
                    { 1, 3, 0m, 1 },
                    { 1, 4, 0m, 2 },
                    { 1, 5, 0m, 2 },
                    { 1, 6, 0m, 2 },
                    { 1, 7, 0m, 2 },
                    { 1, 8, 0m, 1 },
                    { 1, 9, 0m, 2 },
                    { 1, 10, 0m, 1 },
                    { 1, 11, 0m, 1 },
                    { 1, 12, 0m, 1 },
                    { 1, 13, 0m, 1 },
                    { 1, 14, 0m, 1 },
                    { 1, 15, 0m, 2 },
                    { 1, 16, 0m, 1 },
                    { 1, 17, 0m, 1 },
                    { 1, 18, 0m, 2 },
                    { 1, 19, 0m, 1 },
                    { 1, 20, 0m, 1 },
                    { 1, 21, 0m, 1 },
                    { 1, 22, 0m, 2 },
                    { 1, 23, 0m, 1 },
                    { 1, 24, 0m, 1 },
                    { 1, 25, 0m, 2 },
                    { 1, 26, 0m, 2 },
                    { 1, 27, 0m, 1 },
                    { 1, 28, 0m, 2 },
                    { 1, 29, 0m, 1 },
                    { 1, 30, 0m, 2 },
                    { 1, 31, 0m, 1 },
                    { 1, 32, 0m, 1 },
                    { 1, 33, 0m, 1 },
                    { 1, 34, 0m, 1 },
                    { 1, 35, 0m, 2 },
                    { 1, 36, 0m, 2 },
                    { 1, 37, 0m, 1 },
                    { 1, 38, 0m, 2 },
                    { 1, 39, 0m, 2 },
                    { 1, 40, 0m, 2 },
                    { 1, 41, 0m, 1 },
                    { 1, 42, 0m, 2 },
                    { 1, 43, 0m, 2 },
                    { 1, 44, 0m, 2 },
                    { 1, 45, 0m, 1 },
                    { 1, 46, 0m, 1 },
                    { 1, 47, 0m, 2 },
                    { 1, 48, 0m, 2 },
                    { 1, 49, 0m, 2 },
                    { 1, 50, 0m, 1 },
                    { 1, 51, 0m, 1 },
                    { 1, 52, 0m, 1 },
                    { 1, 53, 0m, 1 },
                    { 1, 54, 0m, 1 },
                    { 1, 55, 0m, 2 },
                    { 1, 56, 0m, 1 },
                    { 1, 57, 0m, 1 },
                    { 1, 58, 0m, 2 },
                    { 1, 59, 0m, 1 },
                    { 1, 60, 0m, 2 },
                    { 1, 61, 0m, 1 },
                    { 1, 62, 0m, 1 },
                    { 1, 63, 0m, 2 },
                    { 1, 64, 0m, 1 },
                    { 1, 65, 0m, 2 },
                    { 1, 66, 0m, 2 },
                    { 1, 67, 0m, 2 },
                    { 1, 68, 0m, 2 },
                    { 1, 69, 0m, 1 },
                    { 1, 70, 0m, 1 },
                    { 1, 71, 0m, 1 },
                    { 1, 72, 0m, 1 },
                    { 1, 73, 0m, 1 },
                    { 1, 74, 0m, 1 },
                    { 1, 75, 0m, 2 },
                    { 1, 76, 0m, 1 },
                    { 1, 77, 0m, 1 },
                    { 1, 78, 0m, 2 },
                    { 1, 79, 0m, 1 },
                    { 1, 80, 0m, 1 },
                    { 1, 81, 0m, 1 },
                    { 1, 82, 0m, 2 },
                    { 1, 83, 0m, 1 },
                    { 1, 84, 0m, 1 },
                    { 1, 85, 0m, 1 },
                    { 1, 86, 0m, 2 },
                    { 1, 87, 0m, 1 },
                    { 1, 88, 0m, 2 },
                    { 1, 89, 0m, 2 },
                    { 1, 90, 0m, 2 },
                    { 1, 91, 0m, 2 },
                    { 1, 92, 0m, 2 },
                    { 1, 93, 0m, 1 },
                    { 1, 94, 0m, 1 },
                    { 1, 95, 0m, 2 },
                    { 1, 96, 0m, 2 },
                    { 1, 97, 0m, 1 },
                    { 1, 98, 0m, 2 },
                    { 1, 99, 0m, 2 },
                    { 1, 100, 0m, 1 },
                    { 1, 101, 0m, 2 },
                    { 1, 102, 0m, 2 },
                    { 1, 103, 0m, 2 },
                    { 1, 104, 0m, 2 },
                    { 1, 105, 0m, 1 },
                    { 1, 106, 0m, 1 },
                    { 1, 107, 0m, 2 },
                    { 1, 108, 0m, 1 },
                    { 1, 109, 0m, 1 },
                    { 1, 110, 0m, 2 },
                    { 1, 111, 0m, 2 },
                    { 1, 112, 0m, 2 },
                    { 1, 113, 0m, 2 },
                    { 1, 114, 0m, 1 },
                    { 1, 115, 0m, 1 },
                    { 1, 116, 0m, 2 },
                    { 1, 117, 0m, 2 },
                    { 1, 118, 0m, 2 },
                    { 1, 119, 0m, 2 },
                    { 1, 120, 0m, 1 },
                    { 1, 121, 0m, 1 },
                    { 1, 122, 0m, 2 },
                    { 1, 123, 0m, 2 },
                    { 1, 124, 0m, 2 },
                    { 1, 125, 0m, 2 },
                    { 1, 126, 0m, 1 },
                    { 1, 127, 0m, 1 },
                    { 1, 128, 0m, 2 },
                    { 1, 129, 0m, 2 },
                    { 1, 130, 0m, 1 },
                    { 1, 131, 0m, 2 },
                    { 1, 132, 0m, 1 },
                    { 1, 133, 0m, 1 },
                    { 1, 134, 0m, 2 },
                    { 1, 135, 0m, 2 },
                    { 1, 136, 0m, 2 },
                    { 1, 137, 0m, 1 },
                    { 1, 138, 0m, 2 },
                    { 1, 139, 0m, 2 },
                    { 1, 140, 0m, 1 },
                    { 1, 141, 0m, 2 },
                    { 1, 142, 0m, 1 },
                    { 1, 143, 0m, 2 },
                    { 1, 144, 0m, 1 },
                    { 1, 145, 0m, 2 },
                    { 1, 146, 0m, 2 },
                    { 1, 147, 0m, 2 },
                    { 1, 148, 0m, 1 },
                    { 1, 149, 0m, 2 },
                    { 5, 1, 0m, 1 },
                    { 5, 2, 0m, 1 },
                    { 5, 3, 0m, 1 },
                    { 5, 4, 0m, 1 },
                    { 5, 5, 0m, 1 },
                    { 5, 6, 0m, 2 },
                    { 5, 7, 0m, 1 },
                    { 5, 8, 0m, 2 },
                    { 5, 9, 0m, 1 },
                    { 5, 10, 0m, 1 },
                    { 5, 11, 0m, 1 },
                    { 5, 12, 0m, 2 },
                    { 5, 13, 0m, 2 },
                    { 5, 14, 0m, 2 },
                    { 5, 15, 0m, 2 },
                    { 5, 16, 0m, 2 },
                    { 5, 17, 0m, 1 },
                    { 5, 18, 0m, 2 },
                    { 5, 19, 0m, 2 },
                    { 5, 20, 0m, 2 },
                    { 5, 21, 0m, 2 },
                    { 5, 22, 0m, 1 },
                    { 5, 23, 0m, 1 },
                    { 5, 24, 0m, 2 },
                    { 5, 25, 0m, 1 },
                    { 5, 26, 0m, 1 },
                    { 5, 27, 0m, 1 },
                    { 5, 28, 0m, 1 },
                    { 5, 29, 0m, 2 },
                    { 5, 30, 0m, 2 },
                    { 5, 31, 0m, 2 },
                    { 5, 32, 0m, 1 },
                    { 5, 33, 0m, 2 },
                    { 5, 34, 0m, 1 },
                    { 5, 35, 0m, 1 },
                    { 5, 36, 0m, 1 },
                    { 5, 37, 0m, 2 },
                    { 5, 38, 0m, 2 },
                    { 5, 39, 0m, 2 },
                    { 5, 40, 0m, 1 },
                    { 5, 41, 0m, 2 },
                    { 5, 42, 0m, 2 },
                    { 5, 43, 0m, 2 },
                    { 5, 44, 0m, 2 },
                    { 5, 45, 0m, 1 },
                    { 5, 46, 0m, 2 },
                    { 5, 47, 0m, 1 },
                    { 5, 48, 0m, 2 },
                    { 5, 49, 0m, 2 },
                    { 5, 50, 0m, 1 },
                    { 5, 51, 0m, 1 },
                    { 5, 52, 0m, 2 },
                    { 5, 53, 0m, 1 },
                    { 5, 54, 0m, 1 },
                    { 5, 55, 0m, 2 },
                    { 5, 56, 0m, 2 },
                    { 5, 57, 0m, 1 },
                    { 5, 58, 0m, 2 },
                    { 5, 59, 0m, 1 },
                    { 5, 60, 0m, 2 },
                    { 5, 61, 0m, 1 },
                    { 5, 62, 0m, 1 },
                    { 5, 63, 0m, 2 },
                    { 5, 64, 0m, 1 },
                    { 5, 65, 0m, 1 },
                    { 5, 66, 0m, 1 },
                    { 5, 67, 0m, 1 },
                    { 5, 68, 0m, 1 },
                    { 5, 69, 0m, 1 },
                    { 5, 70, 0m, 2 },
                    { 5, 71, 0m, 1 },
                    { 5, 72, 0m, 1 },
                    { 5, 73, 0m, 2 },
                    { 5, 74, 0m, 1 },
                    { 5, 75, 0m, 2 },
                    { 5, 76, 0m, 2 },
                    { 5, 77, 0m, 1 },
                    { 5, 78, 0m, 2 },
                    { 5, 79, 0m, 1 },
                    { 5, 80, 0m, 2 },
                    { 5, 81, 0m, 1 },
                    { 5, 82, 0m, 1 },
                    { 5, 83, 0m, 2 },
                    { 5, 84, 0m, 2 },
                    { 5, 85, 0m, 1 },
                    { 5, 86, 0m, 1 },
                    { 5, 87, 0m, 2 },
                    { 5, 88, 0m, 2 },
                    { 5, 89, 0m, 1 },
                    { 5, 90, 0m, 1 },
                    { 5, 91, 0m, 1 },
                    { 5, 92, 0m, 2 },
                    { 5, 93, 0m, 1 },
                    { 5, 94, 0m, 2 },
                    { 5, 95, 0m, 2 },
                    { 5, 96, 0m, 1 },
                    { 5, 97, 0m, 1 },
                    { 5, 98, 0m, 1 },
                    { 5, 99, 0m, 2 },
                    { 5, 100, 0m, 1 },
                    { 5, 101, 0m, 1 },
                    { 5, 102, 0m, 1 },
                    { 5, 103, 0m, 1 },
                    { 5, 104, 0m, 1 },
                    { 5, 105, 0m, 1 },
                    { 5, 106, 0m, 2 },
                    { 5, 107, 0m, 2 },
                    { 5, 108, 0m, 1 },
                    { 5, 109, 0m, 2 },
                    { 5, 110, 0m, 2 },
                    { 5, 111, 0m, 1 },
                    { 5, 112, 0m, 2 },
                    { 5, 113, 0m, 2 },
                    { 5, 114, 0m, 1 },
                    { 5, 115, 0m, 1 },
                    { 5, 116, 0m, 2 },
                    { 5, 117, 0m, 1 },
                    { 5, 118, 0m, 1 },
                    { 5, 119, 0m, 1 },
                    { 5, 120, 0m, 1 },
                    { 5, 121, 0m, 2 },
                    { 5, 122, 0m, 2 },
                    { 5, 123, 0m, 1 },
                    { 5, 124, 0m, 2 },
                    { 5, 125, 0m, 2 },
                    { 5, 126, 0m, 1 },
                    { 5, 127, 0m, 2 },
                    { 5, 128, 0m, 1 },
                    { 5, 129, 0m, 2 },
                    { 5, 130, 0m, 1 },
                    { 5, 131, 0m, 1 },
                    { 5, 132, 0m, 1 },
                    { 5, 133, 0m, 1 },
                    { 5, 134, 0m, 2 },
                    { 5, 135, 0m, 1 },
                    { 5, 136, 0m, 2 },
                    { 5, 137, 0m, 2 },
                    { 5, 138, 0m, 1 },
                    { 5, 139, 0m, 1 },
                    { 5, 140, 0m, 1 },
                    { 5, 141, 0m, 2 },
                    { 5, 142, 0m, 1 },
                    { 5, 143, 0m, 2 },
                    { 5, 144, 0m, 1 },
                    { 5, 145, 0m, 1 },
                    { 5, 146, 0m, 2 },
                    { 5, 147, 0m, 1 },
                    { 5, 148, 0m, 2 },
                    { 5, 149, 0m, 2 },
                    { 10, 2, 0m, 2 },
                    { 10, 3, 0m, 2 },
                    { 10, 4, 0m, 2 },
                    { 10, 6, 0m, 2 },
                    { 10, 7, 0m, 2 },
                    { 10, 8, 0m, 2 },
                    { 10, 9, 0m, 1 },
                    { 10, 10, 0m, 1 },
                    { 10, 11, 0m, 2 },
                    { 10, 12, 0m, 1 },
                    { 10, 13, 0m, 2 },
                    { 10, 14, 0m, 2 },
                    { 10, 15, 0m, 2 },
                    { 10, 16, 0m, 1 },
                    { 10, 17, 0m, 2 },
                    { 10, 18, 0m, 2 },
                    { 10, 19, 0m, 2 },
                    { 10, 20, 0m, 2 },
                    { 10, 21, 0m, 1 },
                    { 10, 23, 0m, 1 },
                    { 10, 24, 0m, 2 },
                    { 10, 25, 0m, 1 },
                    { 10, 26, 0m, 2 },
                    { 10, 27, 0m, 2 },
                    { 10, 28, 0m, 2 },
                    { 10, 29, 0m, 2 },
                    { 10, 30, 0m, 2 },
                    { 10, 31, 0m, 1 },
                    { 10, 32, 0m, 2 },
                    { 10, 33, 0m, 2 },
                    { 10, 34, 0m, 1 },
                    { 10, 35, 0m, 2 },
                    { 10, 36, 0m, 1 },
                    { 10, 37, 0m, 1 },
                    { 10, 38, 0m, 2 },
                    { 10, 39, 0m, 1 },
                    { 10, 40, 0m, 1 },
                    { 10, 41, 0m, 2 },
                    { 10, 45, 0m, 2 },
                    { 10, 47, 0m, 2 },
                    { 10, 48, 0m, 2 },
                    { 10, 49, 0m, 1 },
                    { 10, 50, 0m, 2 },
                    { 10, 51, 0m, 1 },
                    { 10, 52, 0m, 2 },
                    { 10, 53, 0m, 2 },
                    { 10, 55, 0m, 2 },
                    { 10, 56, 0m, 1 },
                    { 10, 57, 0m, 2 },
                    { 10, 59, 0m, 1 },
                    { 10, 63, 0m, 2 },
                    { 10, 64, 0m, 2 },
                    { 10, 65, 0m, 2 },
                    { 10, 66, 0m, 1 },
                    { 10, 68, 0m, 1 },
                    { 10, 69, 0m, 2 },
                    { 10, 70, 0m, 2 },
                    { 10, 71, 0m, 1 },
                    { 10, 72, 0m, 2 },
                    { 10, 75, 0m, 2 },
                    { 10, 77, 0m, 1 },
                    { 10, 78, 0m, 2 },
                    { 10, 79, 0m, 1 },
                    { 10, 80, 0m, 1 },
                    { 10, 82, 0m, 2 },
                    { 10, 83, 0m, 1 },
                    { 10, 84, 0m, 1 },
                    { 10, 85, 0m, 1 },
                    { 10, 86, 0m, 2 },
                    { 10, 87, 0m, 1 },
                    { 10, 88, 0m, 1 },
                    { 10, 89, 0m, 1 },
                    { 10, 90, 0m, 2 },
                    { 10, 94, 0m, 2 },
                    { 10, 95, 0m, 2 },
                    { 10, 96, 0m, 1 },
                    { 10, 97, 0m, 1 },
                    { 10, 98, 0m, 2 },
                    { 10, 99, 0m, 1 },
                    { 10, 100, 0m, 2 },
                    { 10, 101, 0m, 2 },
                    { 10, 103, 0m, 2 },
                    { 10, 104, 0m, 2 },
                    { 10, 106, 0m, 2 },
                    { 10, 107, 0m, 2 },
                    { 10, 108, 0m, 1 },
                    { 10, 109, 0m, 2 },
                    { 10, 110, 0m, 1 },
                    { 10, 111, 0m, 1 },
                    { 10, 112, 0m, 1 },
                    { 10, 114, 0m, 1 },
                    { 10, 115, 0m, 2 },
                    { 10, 116, 0m, 1 },
                    { 10, 117, 0m, 1 },
                    { 10, 118, 0m, 2 },
                    { 10, 119, 0m, 2 },
                    { 10, 120, 0m, 1 },
                    { 10, 121, 0m, 2 },
                    { 10, 122, 0m, 1 },
                    { 10, 123, 0m, 1 },
                    { 10, 124, 0m, 2 },
                    { 10, 125, 0m, 1 },
                    { 10, 126, 0m, 1 },
                    { 10, 127, 0m, 2 },
                    { 10, 129, 0m, 1 },
                    { 10, 131, 0m, 1 },
                    { 10, 132, 0m, 1 },
                    { 10, 133, 0m, 1 },
                    { 10, 134, 0m, 1 },
                    { 10, 135, 0m, 2 },
                    { 10, 136, 0m, 2 },
                    { 10, 137, 0m, 1 },
                    { 10, 138, 0m, 1 },
                    { 10, 139, 0m, 2 },
                    { 10, 140, 0m, 1 },
                    { 10, 141, 0m, 1 },
                    { 10, 142, 0m, 1 },
                    { 10, 143, 0m, 1 },
                    { 10, 145, 0m, 2 },
                    { 10, 147, 0m, 2 },
                    { 15, 2, 0m, 2 },
                    { 15, 3, 0m, 2 },
                    { 15, 6, 0m, 2 },
                    { 15, 7, 0m, 2 },
                    { 15, 12, 0m, 2 },
                    { 15, 14, 0m, 2 },
                    { 15, 15, 0m, 2 },
                    { 15, 16, 0m, 2 },
                    { 15, 17, 0m, 2 },
                    { 15, 18, 0m, 2 },
                    { 15, 20, 0m, 2 },
                    { 15, 23, 0m, 2 },
                    { 15, 24, 0m, 2 },
                    { 15, 25, 0m, 2 },
                    { 15, 27, 0m, 1 },
                    { 15, 30, 0m, 1 },
                    { 15, 32, 0m, 1 },
                    { 15, 33, 0m, 1 },
                    { 15, 35, 0m, 1 },
                    { 15, 37, 0m, 1 },
                    { 15, 38, 0m, 2 },
                    { 15, 40, 0m, 2 },
                    { 15, 41, 0m, 1 },
                    { 15, 47, 0m, 1 },
                    { 15, 49, 0m, 2 },
                    { 15, 55, 0m, 2 },
                    { 15, 56, 0m, 1 },
                    { 15, 59, 0m, 1 },
                    { 15, 63, 0m, 1 },
                    { 15, 64, 0m, 2 },
                    { 15, 70, 0m, 2 },
                    { 15, 72, 0m, 2 },
                    { 15, 75, 0m, 2 },
                    { 15, 78, 0m, 1 },
                    { 15, 79, 0m, 1 },
                    { 15, 82, 0m, 1 },
                    { 15, 83, 0m, 2 },
                    { 15, 84, 0m, 1 },
                    { 15, 85, 0m, 1 },
                    { 15, 88, 0m, 1 },
                    { 15, 90, 0m, 2 },
                    { 15, 94, 0m, 2 },
                    { 15, 98, 0m, 2 },
                    { 15, 100, 0m, 2 },
                    { 15, 103, 0m, 2 },
                    { 15, 106, 0m, 2 },
                    { 15, 107, 0m, 2 },
                    { 15, 109, 0m, 1 },
                    { 15, 110, 0m, 1 },
                    { 15, 111, 0m, 1 },
                    { 15, 114, 0m, 2 },
                    { 15, 116, 0m, 1 },
                    { 15, 119, 0m, 2 },
                    { 15, 120, 0m, 2 },
                    { 15, 121, 0m, 1 },
                    { 15, 122, 0m, 1 },
                    { 15, 123, 0m, 1 },
                    { 15, 124, 0m, 1 },
                    { 15, 125, 0m, 1 },
                    { 15, 126, 0m, 1 },
                    { 15, 127, 0m, 1 },
                    { 15, 131, 0m, 1 },
                    { 15, 132, 0m, 2 },
                    { 15, 134, 0m, 1 },
                    { 15, 135, 0m, 1 },
                    { 15, 141, 0m, 2 },
                    { 15, 142, 0m, 1 },
                    { 15, 147, 0m, 1 },
                    { 20, 12, 0m, 1 },
                    { 20, 14, 0m, 1 },
                    { 20, 20, 0m, 2 },
                    { 20, 23, 0m, 2 },
                    { 20, 35, 0m, 2 },
                    { 20, 47, 0m, 1 },
                    { 20, 83, 0m, 2 },
                    { 20, 85, 0m, 2 },
                    { 20, 88, 0m, 1 },
                    { 20, 98, 0m, 1 },
                    { 20, 106, 0m, 2 },
                    { 20, 120, 0m, 2 },
                    { 20, 121, 0m, 1 },
                    { 20, 122, 0m, 1 },
                    { 20, 126, 0m, 1 },
                    { 20, 131, 0m, 2 },
                    { 20, 132, 0m, 2 },
                    { 20, 147, 0m, 1 }
                });

            migrationBuilder.InsertData(
                table: "BookUser",
                columns: new[] { "BooksId", "UsersId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 8 },
                    { 3, 13 },
                    { 4, 3 },
                    { 5, 4 },
                    { 5, 13 },
                    { 6, 5 },
                    { 7, 6 },
                    { 7, 7 },
                    { 8, 7 },
                    { 9, 8 },
                    { 10, 9 },
                    { 11, 10 },
                    { 12, 9 },
                    { 12, 11 },
                    { 13, 12 },
                    { 14, 12 },
                    { 15, 12 },
                    { 16, 13 },
                    { 17, 13 },
                    { 18, 14 },
                    { 19, 1 },
                    { 19, 12 },
                    { 19, 15 },
                    { 20, 2 },
                    { 20, 8 },
                    { 21, 9 },
                    { 22, 10 },
                    { 23, 3 },
                    { 23, 11 },
                    { 24, 2 },
                    { 25, 1 },
                    { 26, 5 },
                    { 27, 6 },
                    { 28, 7 },
                    { 29, 13 },
                    { 30, 15 },
                    { 31, 7 },
                    { 32, 12 },
                    { 33, 9 },
                    { 34, 14 },
                    { 35, 3 },
                    { 35, 5 },
                    { 35, 8 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "BookId", "Comment", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, 25, "Great book but it gave me an existential crisis bigger than I had before", 6, 69 },
                    { 2, 12, "Couldn't put it down, finished it in one sitting!", 14, 91 },
                    { 3, 15, "The plot twists in this book are mind-blowing", 11, 12 },
                    { 4, 16, "A classic that everyone should read", 4, 47 },
                    { 5, 9, "The characters are so well-developed, felt like they were real people", 12, 61 },
                    { 6, 3, "This book made me laugh and cry, a roller coaster of emotions", 1, 38 },
                    { 7, 27, "The writing style is beautiful, every sentence is a work of art", 5, 54 },
                    { 8, 19, "I couldn't guess the ending, kept me guessing until the last page", 11, 55 },
                    { 9, 20, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 1, 53 },
                    { 10, 23, "The themes explored in this book are thought-provoking", 2, 19 },
                    { 11, 29, "The pacing is perfect, kept me engaged from start to finish", 4, 34 },
                    { 12, 10, "This book challenged my perspective on life", 9, 13 },
                    { 13, 23, "The world-building is exceptional, I felt like I was there", 6, 77 },
                    { 14, 11, "A must-read for book lovers", 7, 99 },
                    { 15, 26, "The author's storytelling is captivating", 1, 43 },
                    { 16, 10, "This book is a page-turner, couldn't stop reading", 7, 57 },
                    { 17, 32, "The dialogue between characters is witty and realistic", 2, 57 },
                    { 18, 22, "I've recommended this book to all my friends", 1, 56 },
                    { 19, 21, "It left me with a book hangover, couldn't stop thinking about it", 3, 62 },
                    { 20, 2, "", 12, 40 },
                    { 21, 11, "Couldn't get into the story, found it boring from the start", 3, 75 },
                    { 22, 22, "The characters felt one-dimensional and uninteresting", 7, 38 },
                    { 23, 30, "The plot was predictable, I expected more twists", 14, 74 },
                    { 24, 25, "I didn't connect with the protagonist, lacked depth", 12, 20 },
                    { 25, 23, "The writing style was confusing and hard to follow", 10, 40 },
                    { 26, 32, "This book didn't live up to the hype, very disappointing", 9, 36 },
                    { 27, 2, "The ending felt rushed and unresolved", 2, 97 },
                    { 28, 14, "Too much exposition, not enough action", 12, 83 },
                    { 29, 17, "I found the dialogue unrealistic and forced", 11, 22 },
                    { 30, 8, "The author tried too hard to be profound, came off as pretentious", 13, 13 },
                    { 31, 31, "The pacing was off, some parts dragged on while others felt rushed", 13, 28 },
                    { 32, 11, "The world-building was weak and inconsistent", 6, 79 },
                    { 33, 29, "I couldn't sympathize with any of the characters", 3, 59 },
                    { 34, 24, "The themes explored were cliché and overdone", 14, 15 },
                    { 35, 15, "The book didn't live up to the reviews, a letdown", 1, 64 },
                    { 36, 6, "The grammar and editing were poor, distracting from the story", 12, 44 },
                    { 37, 34, "The book felt like a rip-off of [another popular book]", 4, 67 },
                    { 38, 27, "The author relied too heavily on stereotypes", 4, 94 },
                    { 39, 18, "I regret spending time on this book, wish I chose something else", 8, 54 },
                    { 40, 12, "The climax was anticlimactic, left me unsatisfied", 11, 19 },
                    { 41, 24, "Great book but it gave me an existential crisis bigger than I had before", 10, 42 },
                    { 42, 5, "Couldn't put it down, finished it in one sitting!", 11, 70 },
                    { 43, 5, "The plot twists in this book are mind-blowing", 7, 92 },
                    { 44, 11, "A classic that everyone should read", 3, 65 },
                    { 45, 18, "The characters are so well-developed, felt like they were real people", 2, 27 },
                    { 46, 32, "This book made me laugh and cry, a roller coaster of emotions", 2, 51 },
                    { 47, 21, "The writing style is beautiful, every sentence is a work of art", 14, 75 },
                    { 48, 4, "I couldn't guess the ending, kept me guessing until the last page", 2, 74 },
                    { 49, 21, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 14, 28 },
                    { 50, 6, "The themes explored in this book are thought-provoking", 14, 64 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_GenresId",
                table: "BookGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrders_OrderId",
                table: "BookOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PrimaryGenreId",
                table: "Books",
                column: "PrimaryGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_UsersId",
                table: "BookUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BookId",
                table: "Ratings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BookGenre");

            migrationBuilder.DropTable(
                name: "BookOrders");

            migrationBuilder.DropTable(
                name: "BookUser");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
