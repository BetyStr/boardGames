namespace BusinessLayer.Models;
public class GenreDetail
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<ModelRelated>? Books { get; set; } = new List<ModelRelated>();
}