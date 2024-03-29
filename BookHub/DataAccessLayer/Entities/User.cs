using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entities;

public class User : IdentityUser<int>, IModelRelated
{
    public required string Name { get; set; }
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    
}