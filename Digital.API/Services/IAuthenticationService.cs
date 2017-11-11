using Digital.Contracts;
using System.Threading.Tasks;

namespace Digital.API.Services
{
    public interface IAuthenticationService
    {
        Task<string> GetAuthorizationToken(ApplicationUser user);
    }
}