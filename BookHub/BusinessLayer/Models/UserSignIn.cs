namespace BusinessLayer.Models;

public class UserSignIn
{
    public required string UsernameOrEmail { get; set; }
    public required string Password { get; set; }
}