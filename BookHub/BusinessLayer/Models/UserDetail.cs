namespace BusinessLayer.Models;

public class UserDetail
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public ICollection<ModelRelated> Orders { get; set; } = new List<ModelRelated>();
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
    public ICollection<ModelRelated> Ratings { get; set; } = new List<ModelRelated>();
}