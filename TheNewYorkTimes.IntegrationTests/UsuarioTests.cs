using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewYorkTimes.Interfaces.Repositories;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Tests.Helpers;
using Moq;
using TheNewYorkTimes.Models.InputModels;

namespace TheNewYorkTimes.Tests
{
    public class UsuarioTests
    {
        [Theory]
        [InlineData("João da Silva", "teste@teste.com", "123456", "123456", "user", true)]
        public async void Add_Usuario_ReturnsTrue(string nome, string email, string senha, string confirmeSenha, string role, bool ativo)
        {
            // Arrange
            var usuario = new Usuario(nome, email, senha, confirmeSenha, role, ativo);

            await using var context = new MockUsuarioDb().CreateDbContext();

            // Act
            var result = UsuariosEndPointsV1.AddUsuario(usuario, context);


            // Assert
            Assert.IsType<Created<Usuario>>(result);
            Assert.NotNull(usuario);
            Assert.NotNull(usuario.Nome);
            Assert.NotNull(usuario.Email);
            Assert.NotNull(usuario.Senha);
            Assert.NotNull(usuario.Perfil);
            Assert.NotNull(usuario.Ativo);

            Assert.Collection(context.Usuario, usuario =>
            {
                Assert.Equal(nome, usuario.Nome);
                Assert.Equal(email, usuario.Email);
                Assert.Equal(senha, usuario.Senha);
                Assert.Equal(role, usuario.Perfil);
                Assert.Equal(ativo, usuario.Ativo);
            });
        }


        [Theory]
        [InlineData(1)]
        public async Task GetUsuarioById_ReturnsNotFoundIfNotExists(int idUsuario)
        {
            // Arrange
            await using var context = new MockUsuarioDb().CreateDbContext();

            // Act
            var result = await UsuariosEndPointsV1.GetById(idUsuario, context);

            // Assert
            Assert.IsType<Results<Ok<Usuario>, NotFound>>(result);

            var notFoundResult = (NotFound) result.Result;

            Assert.NotNull(notFoundResult);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetUsuarioById_ReturnsUsuarioFromDatabase(int idUsuario)
        {
            // Arrange
            await using var context = new MockUsuarioDb().CreateDbContext();

            context.Usuario.Add(
                new Usuario
                {
                    Id = 1,
                    Nome = "Test nome 1",
                    Email = "teste1@teste.com",
                    Senha = "123456",
                    ConfirmeSenha = "123456",
                    Perfil = "user",
                    Ativo = true
                });

            await context.SaveChangesAsync();

            // Act
            var result = await UsuariosEndPointsV1.GetById(idUsuario, context);

            //Assert
            Assert.IsType<Results<Ok<Usuario>, NotFound>>(result);

            var okResult = (Ok<Usuario>)result.Result;

            Assert.NotNull(okResult.Value);
            Assert.Equal(idUsuario, okResult.Value.Id);
        }

        [Fact]
        public async Task GetAll_Returns_UsuariosFromDatabase()
        {
            // Arrange
            await using var context = new MockUsuarioDb().CreateDbContext();

            context.Usuario.Add(
                new Usuario
                {
                    Id = 1,
                    Nome = "Test nome 1",
                    Email = "teste1@teste.com",
                    Senha = "123456",
                    ConfirmeSenha = "123456",
                    Perfil = "user",
                    Ativo = true
                });

            context.Usuario.Add(
               new Usuario
               {
                   Id = 2,
                   Nome = "Test nome 2",
                   Email = "teste2@teste.com",
                   Senha = "123456",
                   ConfirmeSenha = "123456",
                   Perfil = "user",
                   Ativo = true
               });

            await context.SaveChangesAsync();

            // Act
            var result = await UsuariosEndPointsV1.GetAll(context);

            // Assert
            Assert.IsType <Ok<Usuario[]>>(result);

            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value);
            Assert.Collection(result.Value, usuario1 =>
            {
                Assert.Equal(1, usuario1.Id);
                Assert.Equal("Test nome 1", usuario1.Nome);
                Assert.Equal("teste1@teste.com", usuario1.Email);
                Assert.Equal("123456", usuario1.Senha);
                Assert.Equal("123456", usuario1.ConfirmeSenha);
                Assert.Equal("user", usuario1.Perfil);
                Assert.True(usuario1.Ativo);
            }, usuario2 =>
            {
                Assert.Equal(2, usuario2.Id);
                Assert.Equal("Test nome 2", usuario2.Nome);
                Assert.Equal("teste2@teste.com", usuario2.Email);
                Assert.Equal("123456", usuario2.Senha);
                Assert.Equal("123456", usuario2.ConfirmeSenha);
                Assert.Equal("user", usuario2.Perfil);
                Assert.True(usuario2.Ativo);
            });
        }
    }
}
