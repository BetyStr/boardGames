namespace BusinessLayer.Models;

public class OrderItem
{
    public int BookId { get; set; }
    public int Count { get; set; }
    public decimal BuyUnitPrice { get; set; }
}