using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity, IModelRelated
{
    public required string Name { get; set; }
    
    public int PublisherId{ get; set; }
    [ForeignKey("PublisherId")] 
    public Publisher Publisher { get; set; } = null!;

    
    public int PrimaryGenreId{ get; set; }
    [ForeignKey("PrimaryGenreId")] 
    public Genre PrimaryGenre { get; set; } = null!;
    
    public int StockInStorage { get; set; }
    public decimal Price { get; set; }
    
    public int OverallRating { get; set; }
    
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<Order> Orders { get; set;} = new List<Order>();
    public ICollection<Author> Authors { get; set; } = new List<Author>();
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<User> Users { get; set;} = new List<User>();
    public ICollection<BookOrder> BookOrders { get; } = new List<BookOrder>();
}