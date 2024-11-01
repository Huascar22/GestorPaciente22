using Modelo.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestorPacienteApi.DTOs.UsuariosDtos
{
    public class GetUsuarioDto
    {
        public string? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
