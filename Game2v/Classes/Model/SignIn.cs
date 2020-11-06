using System.ComponentModel.DataAnnotations;
namespace Game2v.Model
{
    public class SignIn
    {
        [Required]
        [Display(Name = "Nome de Usuário")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Lembrar-se")]
        public bool RememberMe { get; set; }
    }
}