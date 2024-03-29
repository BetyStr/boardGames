using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IAuthService
{
    public Task<AuthToken> Login(UserSignIn userSignIn);
}