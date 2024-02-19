using FluentValidation.TestHelper;
using TheNewYorkTimes.Models;
using TheNewYorkTimes.Validations;

namespace TheNewYorkTimes.UnitTests
{
    public class NoticiaTests
    {
        private readonly NoticiaValidator validator;

        public NoticiaTests()
        {
            validator = new NoticiaValidator();
        }

        #region Titulo

        [Theory]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "10/11/2023", "Teste autor")]
        public void Titulo_Valida_ReturnsTrue(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange
            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(p => p.Titulo);
        }

        [Theory]
        [InlineData("", "Teste descri��o", "Teste chap�u", "10/11/2023", "Teste autor")]
        [InlineData("upelkgzbkykhptrbsfcjvwxkjctvdqvutmzevujfpyojadvufrulnpa", "Teste descri��o", "Teste chap�u", "10/11/2023", "Teste autor")]
        public void Titulo_Valida_ReturnsFalse(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange
            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Titulo);
        }

        #endregion

        #region Descricao

        [Theory]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "10/11/2023", "Teste autor")]
        public void Descricao_Valida_ReturnsTrue(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange
            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(p => p.Descricao);
        }

        [Theory]
        [InlineData("Teste t�tulo", "", "Teste chap�u", "10/11/2023", "Teste autor")]
        [InlineData("Teste t�tulo",
                    "tdhgbnupumfztxskthvqhvfvhedyindutwsljnvqsfuqtecaxakwcqhirxhijzflmulckkezavttuashexjvgyedhirqdjqfpryngnotfwvwobtvgwnzatbqrkysvjavsxofvnjjodldysdtvlzkqjqcclwlrqbygawxwhcmpmyjivvaikddlxpgxsjvjxscfwtdtlmzjejdolqirzrtqsksoywwlmvbgcxtulgyftwwoykrddpktkthclahcroojjhlduamdphldtdiswyfsrkpqqbmphougjgldcrxlzcrkluxcoaqxxyweonwphdrwhgyzxhdbozblqcxljqypqowmtwkcbrjowluevjxhfyjivotbupiojffsrdeiclnjhrknjrglbzhgiupkwvfgirwwakvqurujgmltqdplxehztuoapwjrymrjdrxyijdbxrhkwzdvcvsuqccgxqyzobnddazhiwdeyrwjmrapoojlbuzqarmlmxgcohspp",
            "Teste chap�u", "10/11/2023", "Teste autor")]
        public void Descricao_Valida_ReturnsFalse(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange
            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Descricao);
        }

        #endregion

        #region Chapeu

        [Theory]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "10/11/2023", "Teste autor")]
        public void Chapeu_Valida_ReturnsTrue(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange
            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(p => p.Chapeu);
        }

        [Theory]
        [InlineData("Teste t�tulo", "Teste descri��o", "", "10/11/2023", "Teste autor")]
        [InlineData("Teste t�tulo", "Teste descri��o", "upelkgzbkykhptrbsfcjvwxkjctvdqvutmzevujfpyojadvufrulnpa", "10/11/2023", "Teste autor")]
        public void Chapeu_Valida_ReturnsFalse(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange
            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Chapeu);
        }

        #endregion

        #region DataPublicacao

        [Theory]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "2023-10-11", "Teste autor")]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "2023-11-15", "Teste autor")]
        public void DataPublicacao_Valida_ReturnsTrue(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange

            if (DateTime.Compare(DateTime.Today.Date, dataPublicacao.Date) > 0)
                dataPublicacao = DateTime.Today.Date;

            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(p => p.DataPublicacao);
        }

        [Theory]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "Teste autor")]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "Teste autor", "2023-10-11")]
        public void DataPublicacao_Valida_ReturnsFalse(string titulo, string descricao, string chapeu, string autor, DateTime dataPublicacao = default)
        {
            // Arrange
            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.DataPublicacao);
        }

        #endregion

        #region Autor

        [Theory]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "10/11/2023", "Teste autor")]
        public void Autor_Valida_ReturnsTrue(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange
            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(p => p.Autor);
        }

        [Theory]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "10/11/2023", "")]
        [InlineData("Teste t�tulo", "Teste descri��o", "Teste chap�u", "10/11/2023", "upelkgzbkykhptrbsfcjvwxkjctvdqvutmzevujfpyojadvufrulnpa")]
        public void Autor_Valida_ReturnsFalse(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            // Arrange
            var model = new Noticia(titulo, descricao, chapeu, autor, dataPublicacao);

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Autor);
        }

        #endregion
    }
}