using System.ComponentModel.DataAnnotations;

namespace TheNewYorkTimes.Models.InputModels
{
    public class NoticiaInputModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Chapeu { get; set; }
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }

        public NoticiaInputModel(string titulo, string descricao, string chapeu, string autor, DateTime dataPublicacao)
        {
            Titulo = titulo;
            Descricao = descricao;
            Chapeu = chapeu;
            Autor = autor;
            DataPublicacao = dataPublicacao;
        }
    }
}
