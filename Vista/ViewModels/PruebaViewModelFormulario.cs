using System.ComponentModel.DataAnnotations;

namespace Vista.ViewModels
{
    public class PruebaViewModelFormulario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        public string? Nombre { get; set; }
    }
}
