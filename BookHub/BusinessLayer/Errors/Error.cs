namespace BusinessLayer.Errors;

public enum Error
{
    BookNotFound,
    MultipleBooksNotFound,
    BookFieldEmpty,

    AuthorNotFound,
    MultipleAuthorsNotFound,
    AuthorFieldEmpty,

    GenreNotFound,
    MultipleGenresNotFound,
    GenreFieldEmpty,

    UserNotFound,
    UserAlreadyExists,

    OrderNotFound,
    OrderItemNotFound,
    
    PublisherNotFound,
    RatingNotFound,
}