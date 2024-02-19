using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TheNewYorkTimes.Interfaces.Services;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Services;
using TheNewYorkTimes.Tests.Helpers;

namespace TheNewYorkTimes.Tests
{
    public class LoginTests
    {
        private HashService _serviceHash;

        public LoginTests()
        {
            _serviceHash = new HashService();
        }

        [Theory]
        [InlineData("teste@teste.com")]
        public async Task GetUsuarioByEmail_ReturnsNotFoundIfNotExists(string email)
        {
            // Arrange
            await using var context = new MockUsuarioDb().CreateDbContext();

            // Act
            var result = await UsuariosEndPointsV1.GetByEmail(email, context);

            // Assert
            Assert.IsType<Results<Ok<Usuario>, NotFound>>(result);

            var notFoundResult = (NotFound)result.Result;

            Assert.NotNull(notFoundResult);
        }

        [Theory]
        [InlineData("teste@teste.com", "123456")]
        public async Task GetByEmail_ReturnsUsuarioFromDatabase(string email, string senha)
        {
            // Arrange
            await using var context = new MockUsuarioDb().CreateDbContext();

            context.Usuario.Add(
                new Usuario
                {
                    Id = 1,
                    Nome = "Test nome 1",
                    Email = "teste@teste.com",
                    Senha = CriptografarSenha("123456"),
                    ConfirmeSenha = CriptografarSenha("123456"),
                    Perfil = "user",
                    Ativo = true
                });

            await context.SaveChangesAsync();

            // Act
            var result = await UsuariosEndPointsV1.GetByEmail(email, context);

            //Assert
            Assert.IsType<Results<Ok<Usuario>, NotFound>>(result);

            var okResult = (Ok<Usuario>)result.Result;

            Assert.NotNull(okResult.Value);
            Assert.Equal(email, okResult.Value.Email);
            Assert.Equal(CriptografarSenha(senha), okResult.Value.Senha);
            Assert.True(okResult.Value.Ativo);
        }

        [Theory]
        [InlineData("teste@teste.com")]
        public async Task GetByEmail_ReturnsFalse_UsuarioInativoFromDatabase(string email)
        {
            // Arrange
            await using var context = new MockUsuarioDb().CreateDbContext();

            context.Usuario.Add(
                new Usuario
                {
                    Id = 1,
                    Nome = "Test nome 1",
                    Email = "teste@teste.com",
                    Senha = "123456",
                    ConfirmeSenha = "123456",
                    Perfil = "user",
                    Ativo = false
                });

            await context.SaveChangesAsync();

            // Act
            var result = await UsuariosEndPointsV1.GetByEmail(email, context);

            //Assert
            Assert.IsType<Results<Ok<Usuario>, NotFound>>(result);

            var okResult = (Ok<Usuario>)result.Result;

            Assert.NotNull(okResult.Value);
            Assert.Equal(email, okResult.Value.Email);
            Assert.False(okResult.Value.Ativo);
        }

        [Theory]
        [InlineData("teste@teste.com", "123456")]
        public async Task GetByEmail_ReturnsFalse_UsuarioSenhaNaoConfereFromDatabase(string email, string senha)
        {
            // Arrange
            await using var context = new MockUsuarioDb().CreateDbContext();

            context.Usuario.Add(
                new Usuario
                {
                    Id = 1,
                    Nome = "Test nome 1",
                    Email = "teste@teste.com",
                    Senha = CriptografarSenha("1234567"),
                    ConfirmeSenha = CriptografarSenha("1234567"),
                    Perfil = "user",
                    Ativo = true
                });

            await context.SaveChangesAsync();

            // Act
            var result = await UsuariosEndPointsV1.GetByEmail(email, context);

            //Assert
            Assert.IsType<Results<Ok<Usuario>, NotFound>>(result);

            var okResult = (Ok<Usuario>)result.Result;

            Assert.NotNull(okResult.Value);
            Assert.Equal(email, okResult.Value.Email);
            Assert.NotEqual(CriptografarSenha(senha), okResult.Value.Senha);
            Assert.True(okResult.Value.Ativo);
        }

        private string CriptografarSenha(string senha)
        {
            return _serviceHash.CriptografarSenha(senha);
        }
    }
}
