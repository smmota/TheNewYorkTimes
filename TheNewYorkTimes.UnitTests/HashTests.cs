using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewYorkTimes.Services;

namespace TheNewYorkTimes.UnitTests
{
    public class HashTests
    {
        [Theory]
        [InlineData("123456", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413")]
        public void Senha_Criptografa_ReturnsTrue(string senha, string senhaCripto)
        {
            // Arrange
            var hash = new HashService();

            // Act
            string result = hash.CriptografarSenha(senha);

            // Assert
            Assert.NotNull(senha);
            Assert.Equal(senhaCripto, result);
        }

        [Theory]
        [InlineData("1234567", "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413")]
        public void Senha_Criptografa_ReturnsFalse(string senha, string senhaCripto)
        {
            // Arrange
            var hash = new HashService();

            // Act
            string result = hash.CriptografarSenha(senha);

            // Assert
            Assert.NotNull(senha);
            Assert.NotEqual(senhaCripto, result);
        }
    }
}
