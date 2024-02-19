using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewYorkTimes.Interfaces.Services;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Services;

namespace TheNewYorkTimes.UnitTests
{
    public class TokenTests
    {
        private ITokenService _tokenService;

        public TokenTests()
        {
            _tokenService = new TokenService();
        }

        [Fact]
        public void Token_Generate_ReturnsTrue()
        {
            // Arrange
            var usuario = new Usuario("Teste", "teste@teste.com", "123456", "123456", "user", true) { Id = 1 };

            // Act
            var token = _tokenService.GenereteToken(usuario);

            // Assert
            Assert.NotEmpty(token);
        }

        [Fact]
        public void Token_Generate_ReturnsFalse()
        {
            // Arrange
            var usuario = new Usuario();

            // Act
            var token = _tokenService.GenereteToken(usuario);

            // Assert
            Assert.Empty(token);
        }
    }
}
