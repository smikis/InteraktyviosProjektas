using Digital.Models;

namespace Digital.Services
{
    public interface IAuthenticationService
    {
        string GetAuthorizationToken(ApplicationUser user);
    }
}