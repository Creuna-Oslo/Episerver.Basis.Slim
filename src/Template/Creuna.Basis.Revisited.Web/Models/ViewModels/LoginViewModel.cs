using System.ComponentModel.DataAnnotations;

namespace Creuna.Basis.Revisited.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public bool PersistLogin { get; set; }
    }
}