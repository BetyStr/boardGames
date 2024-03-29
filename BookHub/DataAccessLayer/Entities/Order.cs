using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<BookOrder> BookOrders { get; } = new List<BookOrder>();
    
}