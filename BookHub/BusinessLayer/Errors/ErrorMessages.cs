using DataAccessLayer.Entities;

namespace BusinessLayer.Errors;


public static class ErrorMessages
{
    public static (Error err, string message) BooksEmpty() => (Error.BookFieldEmpty ,"Book field cannot be empty");
    public static (Error err, string message) BookNotFound() => (Error.MultipleBooksNotFound,"One or more books could not be found");
    public static (Error err, string message) BookNotFound(int id) => (Error.BookNotFound, $"Book with 'ID={id}' could not be found");
    public static (Error err, string message) BookNotFound(int id, string name) => (Error.BookNotFound, $"Book 'Name={name}' <OR> 'ID={id}' could not be found");
    
    
    public static (Error err, string message) AuthorsEmpty() => (Error.AuthorFieldEmpty,"Author field cannot be empty");
    public static (Error err, string message) AuthorNotFound() => (Error.MultipleAuthorsNotFound,"One or more authors could not be found");
    public static (Error err, string message) AuthorNotFound(int id) => (Error.AuthorNotFound,$"Author with 'ID={id}' could not be found");
    
    
    public static (Error err, string message) GenresEmpty() => (Error.GenreFieldEmpty,"Genre field cannot be empty");
    public static (Error err, string message) GenreNotFound() => (Error.MultipleGenresNotFound ,"One or more genre could not be found");
    public static (Error err, string message) GenreNotFound(int id) => (Error.GenreNotFound ,$"Genre with 'ID={id}' could not be found");
    public static (Error err, string message) GenreNotFound(int id, string name) => (Error.GenreNotFound, $"Genre 'Name={name}' <OR> 'ID={id}' could not be found");
    
    
    public static (Error err, string message) PublisherNotFound(int id) => (Error.PublisherNotFound , $"Publisher with 'ID={id}' could not be found");
    public static (Error err, string message) PublisherNotFound(int id, string name) => (Error.PublisherNotFound, $"Publisher 'Name={name}' <OR> 'ID={id}' could not be found");
    
    
    public static (Error err, string message) UserNotFound(int id) => (Error.UserNotFound,$"User with 'ID={id}' has no unpaid orders");
    public static (Error err, string message) UserNotFound(int id, string name) => (Error.UserNotFound, $"User 'Name={name}' <OR> 'ID={id}' could not be found");
    public static (Error err, string message) UserAlreadyExists(string name) => (Error.UserAlreadyExists, $"User 'Name={name}' already exists");
    
    
    public static (Error err, string message) OrderNotFound(int id) => (Error.OrderNotFound, $"Order with 'ID={id}' could not be found");
    public static (Error err, string message) OrderNotFound(int userId, OrderStatus orderStatus) => (Error.OrderNotFound, $"Order for User 'ID={userId}' with OrderStatus: {orderStatus.ToString()} could not be found");
    public static (Error err, string message) OrderItemNotFound(int userId, int bookId) => (Error.OrderItemNotFound, $"Order Item for User 'ID={userId}' that contains Book 'ID={bookId}' could not be found");
    
    public static (Error err, string message) RatingNotFound(int id) => (Error.RatingNotFound, $"Rating with 'ID={id}' could not be found");
}