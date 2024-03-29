namespace BusinessLayer.Models;

public class UserCreate
{
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();

}