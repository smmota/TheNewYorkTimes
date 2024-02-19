namespace TheNewYorkTimes.Models
{
    public class Login : BaseModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public Login() { }

        public Login(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
