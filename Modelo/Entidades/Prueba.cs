using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Entidades
{
    public class Prueba
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public List<ExamenMedico>? ExamenMedico { get; set; }
    }
}


