using System.ComponentModel.DataAnnotations;

namespace GestorPacienteApi.DTOs.PruebasDto
{
    public class PostPruebaDto
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        public string? Nombre { get; set; }
    }
}
