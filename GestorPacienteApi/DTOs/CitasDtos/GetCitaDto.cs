using Modelo.Entidades;
using Modelo.Enums;

namespace GestorPacienteApi.DTOs.CitasDtos
{
    public class GetCitaDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime FechaCita { get; set; }
        public string? Causa { get; set; }
        public EstadoCita EstadoCita { get; set; }
    }
}
