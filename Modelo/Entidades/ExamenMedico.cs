using Modelo.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Entidades
{
    public class ExamenMedico
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdPrueba { get; set; }
        public Paciente? Paciente { get; set; }
        public Prueba? Prueba { get; set; }
        public EstadoResultado EstadoResultado { get; set; }
        public string? Resultado { get; set; }
        [NotMapped]
        public bool BuscardoCedula { get; set; }
    }
}

