using Modelo.Entidades;

namespace GestorPacienteApi.DTOs.CitasDtos
{
    public class PostCitaDto
    {
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime FechaCita { get; set; }
        public string? Causa { get; set; }
    }
}
