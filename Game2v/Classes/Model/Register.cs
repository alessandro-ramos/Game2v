using System;
using System.ComponentModel.DataAnnotations;

namespace Game2v.Model
{
    public class Register
    {
        [Required]
        [Display(Name = "Nome de Usuário")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [Display(Name = "Confirme a Senha")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Nome Completo")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }
    }
}