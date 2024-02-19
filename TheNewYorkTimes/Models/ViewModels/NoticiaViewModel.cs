using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TheNewYorkTimes.Models.ViewModels
{
    public class NoticiaViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Chapeu { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }

        public NoticiaViewModel(string titulo, string descricao, string chapeu, DateTime dataPublicacao, string autor)
        {
            Titulo = titulo;
            Descricao = descricao;
            Chapeu = chapeu;
            DataPublicacao = dataPublicacao;
            Autor = autor;
        }
    }
}
