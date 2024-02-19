using FluentValidation.TestHelper;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Validations;

namespace TheNewYorkTimes.UnitTests
{
    public class LoginTests
    {
        private readonly LoginValidator validator;

        public LoginTests()
        {
            validator = new LoginValidator();
        }

        #region Email

        [Theory]
        [InlineData("teste@teste.com", "")]
        public void Email_Valida_ReturnsTrue(string email, string senha)
        {
            // Arrange
            var model = new Login(email, senha);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(p => p.Email);
        }

        [Theory]
        [InlineData("teste.com", "")]
        [InlineData("teste@", "")]
        [InlineData("upelkgzbkykhptrbsfcjvwxkjctvdqvutmzevujfpyojadvufrulnpa", "")]
        [InlineData("", "")]
        public void Email_Valida_ReturnsFalse(string email, string senha)
        {
            // Arrange
            var model = new Login(email, senha);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Email);
        }

        #endregion

        #region Senha

        [Theory]
        [InlineData("", "123456")]
        public void Senha_Valida_ReturnsTrue(string email, string senha)
        {
            // Arrange
            var model = new Login(email, senha);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(p => p.Senha);
        }

        [Theory]
        [InlineData("", "12345678901")]
        [InlineData("", "")]
        public void Senha_Valida_ReturnsFalse(string email, string senha)
        {
            // Arrange
            var model = new Login(email, senha);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Senha);
        }

        #endregion
    }
}
