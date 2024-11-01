using System.ComponentModel.DataAnnotations;

namespace GestorPacienteApi.DTOs.MedicosDto
{
    public class PatchMedicoDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Cedula { get; set; }
    }
}
