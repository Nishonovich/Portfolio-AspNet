using Portfolio.WebApi.Models;

namespace Portfolio.WebApi.Interfaces.Services
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
