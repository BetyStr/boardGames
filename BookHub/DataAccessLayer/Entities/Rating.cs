using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Rating : BaseEntity
{
    public int UserId { get; set; }
    [ForeignKey("UserId")] 
    public User User { get; set; } = null!;
    
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; } = null!;
    
    public int Value { get; set; }
    
    public string? Comment { get; set; }
}