using Microsoft.AspNetCore.Mvc.Rendering;
using Modelo.Entidades;

namespace Vista.ViewModels
{
    public class ExamenViewModel
    {
        public List<Prueba>? Pruebas { get; set; } = new List<Prueba>();
        public List<Paciente>? Pacientes { get; set; } = new List<Paciente>();
        public int SelectedPruebaId { get; set; }
        public int SelectedPacienteId { get; set; }
    }
}
