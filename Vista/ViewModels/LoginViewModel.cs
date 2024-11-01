using System.ComponentModel.DataAnnotations;

namespace Vista.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name ="Usuario")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Clave")]
        public string? Password { get; set; }
        public bool Recordar { get; set; }
        public bool AcessoDenegado { get; set; }
    }
}
