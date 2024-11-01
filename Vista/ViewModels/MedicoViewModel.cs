using System.ComponentModel.DataAnnotations;

namespace Vista.ViewModels
{
    public class MedicoViewModel
    {
        public int Id { get; set; }
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
        public IFormFile? File { get; set; }
        public string? Foto { get; set; }
    }
}
