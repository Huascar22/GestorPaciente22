using System.ComponentModel.DataAnnotations;

namespace GestorPacienteApi.DTOs.MedicosDto
{
    public class PostMedicoDto
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        public string? Correo { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "La {0} es requerida")]
        public string? Cedula { get; set; }
    }
}
