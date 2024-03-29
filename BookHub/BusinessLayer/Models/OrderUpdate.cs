using DataAccessLayer.Entities;

namespace BusinessLayer.Models;

public class OrderUpdate
{
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}