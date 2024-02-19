using System.ComponentModel.DataAnnotations;

namespace TheNewYorkTimes.Models.InputModels
{
    public class LoginInputModel
    {
        public string Email { get; set; }

        public string Senha { get; set; }

        public LoginInputModel(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
