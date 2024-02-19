namespace TheNewYorkTimes.Models.ViewModels
{
    public class UsuariosViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
        public bool Ativo { get; set; }

        public UsuariosViewModel(int id, string nome, string email, string perfil, bool ativo)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Perfil = perfil;
            Ativo = ativo;
        }

        public UsuariosViewModel()
        {
            
        }
    }
}
