using Microsoft.AspNetCore.Http.HttpResults;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Tests.Helpers;

namespace TheNewYorkTimes.Tests
{
    public class NoticiaIntegrationTests
    {
        [Theory]
        [InlineData("Teste título", "Teste descrição", "Teste chapéu", "10/11/2023", "Teste autor")]
        public async void Add_Noticia_ReturnsTrue(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange
            var noticia = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            await using var context = new MockNoticiaDb().CreateDbContext();

            // Act
            var result = NoticiasEndPointsV1.AddNoticia(noticia, context);


            // Assert
            Assert.IsType<Created<Noticia>>(result);
            Assert.NotNull(noticia);
            Assert.NotNull(noticia.Titulo);
            Assert.NotNull(noticia.Descricao);
            Assert.NotNull(noticia.Chapeu);
            Assert.NotNull(noticia.Autor);

            Assert.Collection(context.Noticia, noticia =>
            {
                Assert.Equal(titulo, noticia.Titulo);
                Assert.Equal(descricao, noticia.Descricao);
                Assert.Equal(chapeu, noticia.Chapeu);
                Assert.Equal(autor, noticia.Autor);
                Assert.Equal(dataPublicacao, noticia.DataPublicacao);
            });
        }

        [Theory]
        [InlineData(1)]
        public async Task GetNoticiaById_ReturnsNotFoundIfNotExists(int idNoticia)
        {
            // Arrange
            await using var context = new MockNoticiaDb().CreateDbContext();

            // Act
            var result = await NoticiasEndPointsV1.GetById(idNoticia, context);

            // Assert
            Assert.IsType<Results<Ok<Noticia>, NotFound>>(result);

            var notFoundResult = (NotFound)result.Result;

            Assert.NotNull(notFoundResult);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetNoticiaById_ReturnsNoticiaFromDatabase(int idNoticia)
        {
            // Arrange
            await using var context = new MockNoticiaDb().CreateDbContext();

            context.Noticia.Add(
                new Noticia
                {
                    Id = 1,
                    Titulo = "Teste título 1",
                    Descricao = "Teste descrição 1",
                    Chapeu = "Teste chapéu 1",
                    DataPublicacao = Convert.ToDateTime("2023-11-15").Date,
                    Autor = "Teste autor 1"
                });

            await context.SaveChangesAsync();

            // Act
            var result = await NoticiasEndPointsV1.GetById(idNoticia, context);

            //Assert
            Assert.IsType<Results<Ok<Noticia>, NotFound>>(result);

            var okResult = (Ok<Noticia>)result.Result;

            Assert.NotNull(okResult.Value);
            Assert.Equal(idNoticia, okResult.Value.Id);
        }

        [Fact]
        public async Task GetAll_Returns_NoticiasFromDatabase()
        {
            // Arrange
            await using var context = new MockNoticiaDb().CreateDbContext();

            context.Noticia.Add(
                new Noticia
                {
                    Id = 1,
                    Titulo = "Teste título 1",
                    Descricao = "Teste descrição 1",
                    Chapeu = "Teste chapéu 1",
                    DataPublicacao = Convert.ToDateTime("2023-11-15").Date,
                    Autor = "Teste autor 1"
                });

            context.Noticia.Add(
               new Noticia
               {
                   Id = 2,
                   Titulo = "Teste título 2",
                   Descricao = "Teste descrição 2",
                   Chapeu = "Teste chapéu 2",
                   DataPublicacao = Convert.ToDateTime("2023-11-20").Date,
                   Autor = "Teste autor 2"
               });

            await context.SaveChangesAsync();

            // Act
            var result = await NoticiasEndPointsV1.GetAll(context);

            // Assert
            Assert.IsType<Ok<Noticia[]>>(result);

            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value);
            Assert.Collection(result.Value, noticia1 =>
            {
                Assert.Equal(1, noticia1.Id);
                Assert.Equal("Teste título 1", noticia1.Titulo);
                Assert.Equal("Teste descrição 1", noticia1.Descricao);
                Assert.Equal("Teste chapéu 1", noticia1.Chapeu);
                Assert.Equal(Convert.ToDateTime("2023-11-15").Date, noticia1.DataPublicacao);
                Assert.Equal("Teste autor 1", noticia1.Autor);
            }, noticia2 =>
            {
                Assert.Equal(2, noticia2.Id);
                Assert.Equal("Teste título 2", noticia2.Titulo);
                Assert.Equal("Teste descrição 2", noticia2.Descricao);
                Assert.Equal("Teste chapéu 2", noticia2.Chapeu);
                Assert.Equal(Convert.ToDateTime("2023-11-20").Date, noticia2.DataPublicacao);
                Assert.Equal("Teste autor 2", noticia2.Autor);
            });
        }
    }
}