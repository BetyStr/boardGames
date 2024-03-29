using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mapper;

public static class EntityMapper
{
  
    public static ModelRelated MapModelToRelated<T>(T model) where T: IModelRelated
    {
        return new ModelRelated
        {
            Id = model.Id,
            Name = model.Name
        };
    }

    public static OrderItem MapBookOrderToOrderItem(BookOrder bookOrder)
    {
        return new OrderItem
        {
            BookId = bookOrder.BookId,
            Count = bookOrder.Count,
            BuyUnitPrice = bookOrder.BuyUnitPrice
        };
    }
    
    public static AuthorDetail MapAuthorToAuthorDetail(Author author)
    {
        return new AuthorDetail
        {
            Id = author.Id,
            Name = author.Name,
            Books = author.Books.Select(MapModelToRelated).ToList()
        };
    }
   
    public static GenreDetail MapGenreToGenreDetail(Genre genre)
    {
        return new GenreDetail
        {
            Id = genre.Id,
            Name = genre.Name,
            Books = genre.Books.Select(MapModelToRelated).ToList()
        };
    }
    
    public static PublisherDetail MapPublisherToPublisherDetail(Publisher publisher)
    {
        return new PublisherDetail
        {
            Id = publisher.Id,
            Name = publisher.Name,
            Books = publisher.Books.Select(MapModelToRelated).ToList()
        };
    }
    public static BookDetail MapBookToBookDetail(Book book)
    {
        return new BookDetail
        {
            Id = book.Id,
            Name = book.Name,
            PrimaryGenre = MapModelToRelated(book.PrimaryGenre),
            Genres = book.Genres.Select(MapModelToRelated).ToList(),
            Publisher = MapModelToRelated(book.Publisher),
            Authors = book.Authors.Select(MapModelToRelated).ToList(),
            OverallRating = book.OverallRating,
            Price = book.Price,
            StockInStorage = book.StockInStorage
        };
    }
    public static RatingDetail MapRatingToRatingDetail(Rating rating)
    {
        return new RatingDetail
        {
            Id = rating.Id,
            Book = MapModelToRelated(rating.Book),
            User = MapModelToRelated(rating.User),
            Value = rating.Value,
            Comment = rating.Comment
        };
    }
    public static OrderDetail MapOrderToOrderDetail(Order order)
    {
        return new OrderDetail
        {
            Id = order.Id,
            User = MapModelToRelated(order.User),
            TotalPrice = order.TotalPrice,
            OrderStatus = order.OrderStatus,
            Date = order.Date,
            Books = order.Books.Select(MapModelToRelated).ToList(),
            OrderItems = order.BookOrders.Where(bo => bo.OrderId == order.Id).Select(MapBookOrderToOrderItem).ToList()
        };
    }
    
    public static UserDetail MapUserToUserDetail(User user)
    {
        return new UserDetail
        {
            Id = user.Id,
            Name = user.Name, 
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            Books = user.Books.Select(MapModelToRelated).ToList(),
        };
    }
    
    public static ModelRelated MapBookDetailToRelated(BookDetail model)
    {
        return new ModelRelated
        {
            Id = model.Id,
            Name = model.Name
        };
    }
    
    public static ModelRelated MapUserDetailToRelated(UserDetail model)
    {
        return new ModelRelated
        {
            Id = model.Id,
            Name = model.Name
        };
    }

    public static BookCreate MapBookDetailToCreate(BookDetail book)
    {
        return new BookCreate
        {
            Name = book.Name,
            Authors = book.Authors,
            Publisher = book.Publisher,
            PrimaryGenre = book.PrimaryGenre,
            Genres = book.Genres,
            OverallRating = book.OverallRating,
            StockInStorage = book.StockInStorage,
            Price = book.Price
        };
    }
}