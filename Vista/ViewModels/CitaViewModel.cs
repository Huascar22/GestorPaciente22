using Modelo.Entidades;

namespace Vista.ViewModels
{
    public class CitaViewModel
    {
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public List<Paciente>? Pacientes { get; set; }
        public List<Medico>? Medicos { get; set; }
        public DateTime FechaCita { get; set; }
        public string? Causa { get; set; }
    }
}
