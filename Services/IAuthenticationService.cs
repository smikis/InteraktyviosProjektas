using Digital.Models;
using System.Threading.Tasks;

namespace Digital.Services
{
    public interface IAuthenticationService
    {
        Task<string> GetAuthorizationToken(ApplicationUser user);
    }
}