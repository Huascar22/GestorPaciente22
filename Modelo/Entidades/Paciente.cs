using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Entidades
{
    public class Paciente
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Cedula  { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Foto { get; set; }
        public List<Cita>? Citas { get; set; }
        public List<ExamenMedico>? ExamenMedico { get; set; }
    }
}

