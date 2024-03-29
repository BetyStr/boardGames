namespace BusinessLayer.Models;

public class OrderCreate
{
    public required ModelRelated User { get; set; }
    public decimal TotalPrice { get; set; }
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}