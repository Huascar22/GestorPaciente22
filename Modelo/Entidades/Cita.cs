using Modelo.Enums;

namespace Modelo.Entidades
{
    public class Cita
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }
        public DateTime FechaCita { get; set; }
        public string? Causa { get; set; }
        public EstadoCita EstadoCita { get; set; }
    }
}
