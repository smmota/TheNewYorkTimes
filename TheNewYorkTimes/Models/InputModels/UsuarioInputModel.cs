using System.ComponentModel.DataAnnotations;

namespace TheNewYorkTimes.Models.InputModels
{
    public class UsuarioInputModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmeSenha { get; set; }

        public UsuarioInputModel(string nome, string email, string senha, string confirmeSenha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            ConfirmeSenha = confirmeSenha;
        }
    }
}
