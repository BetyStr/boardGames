using Microsoft.Build.Framework;

namespace BusinessLayer.Models;

public class BookCreate
{
    [Required]
    public string Name { get; set; }
    [Required]
    public ModelRelated PrimaryGenre { get; set; }
    [Required]
    public ICollection<ModelRelated> Genres { get; set; } = new List<ModelRelated>();
    [Required]
    public ModelRelated Publisher { get; set; }
    [Required]
    public int StockInStorage { get; set; }
    public int OverallRating { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public ICollection<ModelRelated> Authors { get; set; } = new List<ModelRelated>();
}