namespace DataAccessLayer.Entities;

public class BookOrder
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public int Count { get; set; }
    
    public decimal BuyUnitPrice { get; set; }
}