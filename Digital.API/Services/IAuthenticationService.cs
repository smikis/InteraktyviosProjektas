using Digital.Contracts;
using System.Threading.Tasks;

namespace Digital.API.Services
{
    public interface ITokenService
    {
        Task<string> GetAuthorizationToken(ApplicationUser user);
    }
}