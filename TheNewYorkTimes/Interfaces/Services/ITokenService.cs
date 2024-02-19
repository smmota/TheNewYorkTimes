using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Interfaces.Services
{
    public interface ITokenService
    {
        string GenereteToken(Usuario usuario);
    }
}
