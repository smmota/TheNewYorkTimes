using FluentValidation.TestHelper;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Validations;

namespace TheNewYorkTimes.UnitTests
{
    public class UsuarioTests
    {
        private readonly UsuarioValidator validator;

        public UsuarioTests()
        {
            validator = new UsuarioValidator();
        }

        #region Nome        

        [Theory]
        [InlineData("João da Silva", "teste@teste.com", "123456", "123456", "", true)]
        public void Nome_Valida_ReturnsTrue(string nome, string email, string senha, string confirmeSenha, string role, bool ativo)
        {
            // Arrange
            var model = new Usuario(nome, email, senha, confirmeSenha, role, ativo);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(p => p.Nome);
        }

        [Theory]
        [InlineData("", "teste@teste.com", "123456", "123456", "", true)]
        [InlineData("upelkgzbkykhptrbsfcjvwxkjctvdqvutmzevujfpyojadvufrulnpa", "teste@teste.com", "123456", "123456", "", true)]
        public void Nome_Valida_ReturnsFalse(string nome, string email, string senha, string confirmeSenha, string role, bool ativo)
        {
            // Arrange
            var model = new Usuario(nome, email, senha, confirmeSenha, role, ativo);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Nome);
        }

        #endregion

        #region ConfirmeSenha        

        [Theory]
        [InlineData("João da Silva", "teste@teste.com", "123456", "123456", "", true)]
        public void ConfirmeSenha_Valida_ReturnsTrue(string nome, string email, string senha, string confirmeSenha, string role, bool ativo)
        {
            // Arrange
            var model = new Usuario(nome, email, senha, confirmeSenha, role, ativo);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(p => p.ConfirmeSenha);
        }

        [Theory]
        [InlineData("João da Silva", "teste@teste.com", "123456", "", "", true)]
        [InlineData("João da Silva", "teste@teste.com", "123456", "1234567", "", true)]
        [InlineData("João da Silva", "teste@teste.com", "123456", "1234567890", "", true)]
        public void ConfirmeSenha_Valida_ReturnsFalse(string nome, string email, string senha, string confirmeSenha, string role, bool ativo)
        {
            // Arrange
            var model = new Usuario(nome, email, senha, confirmeSenha, role, ativo);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.ConfirmeSenha);
        }

        #endregion
    }
}
