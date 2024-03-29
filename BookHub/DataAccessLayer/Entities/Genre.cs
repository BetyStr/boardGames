namespace DataAccessLayer.Entities;

public class Genre : BaseEntity, IModelRelated
{
    public required string Name { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Book> PrimaryGenreBooks { get; set; } = new List<Book>();
}