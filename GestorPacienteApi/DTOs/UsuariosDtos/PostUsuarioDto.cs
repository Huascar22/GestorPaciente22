using Microsoft.AspNetCore.Antiforgery;
using Modelo.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestorPacienteApi.DTOs.UsuariosDtos
{
    public class PostUsuarioDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre de Usuario")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Clave")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\W).{6,}$",
            ErrorMessage = "La clave debe de tener al menos 6 digitos, una mayuscula, una minucula y un caracter especial")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las claves no son iguales")]
        [Display(Name = "Confirmacion de Clave")]
        public string? ConfirPassword { get; set; }

        [Display(Name = "Tipo Usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public TipoUsuario TipoUsuario { get; set; }
    }
}
