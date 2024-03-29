namespace BusinessLayer.Models;

public class PublisherDetail 
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public ICollection<ModelRelated>? Books { get; set; } = new List<ModelRelated>();
}