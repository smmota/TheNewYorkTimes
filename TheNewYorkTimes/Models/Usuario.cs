using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheNewYorkTimes.Models
{
    public class Usuario : Login
    {
        [NotMapped]
        public string ConfirmeSenha { get; set; }
        public string Nome { get; set; }
        public string Perfil { get; set; }
        public bool Ativo { get; set; }

        public Usuario() { }

        public Usuario(string nome, string email, string senha, string confirmeSenha, string perfil, bool ativo)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            ConfirmeSenha = confirmeSenha;
            Perfil = perfil;
            Ativo = ativo;
        }
    }
}
