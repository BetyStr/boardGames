namespace BusinessLayer.Models;

public class AuthToken
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }

}