namespace TheNewYorkTimes.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmeSenha { get; set; }
        public string Perfil { get; set; }
        public bool Ativo { get; set; }

        public UsuarioViewModel(string nome, string email, string senha, string confirmeSenha, string perfil, bool ativo)
        {
            Nome = nome;
            Email = email; 
            Senha = senha;
            ConfirmeSenha = confirmeSenha;
            Perfil = perfil;
            Ativo = ativo;
        }

        public UsuarioViewModel()
        {
            Perfil = "user";
            Ativo = true;
        }   
    }
}
