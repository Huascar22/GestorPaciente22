using System.ComponentModel.DataAnnotations;

namespace Vista.ViewModels
{
    public class RecuperarClaveViewModel
    {
        [Required(ErrorMessage ="El {0} es requerido")]
        public string? Correo { get; set; }
        public bool UsuarioNull;
    }
}
