using System.ComponentModel.DataAnnotations;

namespace DemoLogin.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha inválida")]
        public string Password { get; set; }
    }
}