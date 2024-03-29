namespace BusinessLayer.Models;

public class BookDetail
{
    public required int Id { get; set; }
    public required string Name { get; init; }
    public required ModelRelated PrimaryGenre{ get; set; }
    public ICollection<ModelRelated> Genres { get; init; } = new List<ModelRelated>();
    public required ModelRelated Publisher { get; init; }
    public int StockInStorage { get; init; }
    public int OverallRating { get; set; }
    public decimal Price { get; init; }
    public ICollection<ModelRelated> Authors { get; init; } = new List<ModelRelated>();
}