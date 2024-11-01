using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace Vista.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string? Correo { get; set; }
        public string? Code { get; set; }

        [Required(ErrorMessage ="La {0} es requerida")]
        [Display(Name ="Clave")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Clave de confirmacion")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirPassword { get; set; }
    }
}
