using Modelo.Entidades;
using Modelo.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorPacienteApi.DTOs.ExamenDto
{
    public class GetExamenDto
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdPrueba { get; set; }
        public EstadoResultado EstadoResultado { get; set; }
    }
}
