namespace DataAccessLayer.Entities;

public class Publisher : BaseEntity, IModelRelated
{
    public required string Name { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}