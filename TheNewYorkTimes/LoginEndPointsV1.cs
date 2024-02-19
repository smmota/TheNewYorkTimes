using Microsoft.AspNetCore.Http.HttpResults;
using TheNewYorkTimes.Data;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes
{
    public class LoginEndPointsV1
    {
        public static Created<Login> DoLogin(Login login, UsuarioDbContext context)
        {
            return null;
        }
    }
}
