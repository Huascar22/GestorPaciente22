using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorPacienteApi.DTOs.LoginDto
{
    public class PostLoginDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Usuario")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Clave")]
        public string? Password { get; set; }
    }
}
