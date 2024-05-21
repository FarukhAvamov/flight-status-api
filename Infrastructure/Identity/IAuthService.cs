namespace Infrastructure.Identity;

public interface IAuthService
{
    Task<string> Authenticate(string username, string password);
    Task Register(string username, string password, string roleCode);
}