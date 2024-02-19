using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheNewYorkTimes.Interfaces.Services;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration Configuration { get; }

        public TokenService()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .AddEnvironmentVariables().Build();
        }

        public string GenereteToken(Usuario usuario)
        {
            if (usuario == null || (usuario.Id == 0 && string.IsNullOrEmpty(usuario.Nome) && string.IsNullOrEmpty(usuario.Perfil) && string.IsNullOrEmpty(usuario.Email)))
                return string.Empty;

            var key = Encoding.ASCII.GetBytes(Configuration["Keys:KeyToken"] ?? string.Empty);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Perfil.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                                                    new SymmetricSecurityKey(key),
                                                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
