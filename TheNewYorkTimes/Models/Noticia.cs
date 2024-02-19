using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheNewYorkTimes.Validations;

namespace TheNewYorkTimes.Models
{
    public class Noticia : BaseModel
    {        
        [MaxLength(50)]
        [Required(ErrorMessage = "Informe o título da notícia")]
        public string Titulo { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage = "Informe a descrição da notícia")]
        public string Descricao { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Informe o assunto da notícia")]
        public string Chapeu { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Informe a data de publicação da notícia")]
        public DateTime DataPublicacao { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe o autor da notícia")]
        [Date(ErrorMessage = "Data inválida")]
        public string Autor { get; set; }

        public Noticia() { }

        public Noticia(string titulo, string descricao, string chapeu, string autor, DateTime dataPublicacao = default(DateTime))
        {
            Titulo = titulo;
            Descricao = descricao;
            Chapeu = chapeu;
            Autor = autor;
            DataPublicacao = dataPublicacao;
        }        
    }
}
