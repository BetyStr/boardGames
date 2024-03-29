namespace BusinessLayer.Models;

public class AuthorUpdate
{
    public required string Name { get; set; }
    public ICollection<ModelRelated>? Books { get; set; }
}