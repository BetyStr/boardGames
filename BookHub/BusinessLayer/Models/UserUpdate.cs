namespace BusinessLayer.Models;

public class UserUpdate
{
    public required string Name { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
    
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();

}