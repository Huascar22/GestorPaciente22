using Modelo.Entidades;

namespace GestorPacienteApi.DTOs.PacientesDtos
{
    public class GetPacienteDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Cedula { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
