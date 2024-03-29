namespace BusinessLayer.Models;

public class PublisherUpdate
{
    public required string Name { get; set; }
    public ICollection<ModelRelated>? Books { get; set; } = new List<ModelRelated>();
}