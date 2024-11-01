using Microsoft.AspNetCore.Mvc.Rendering;
using Modelo.Entidades;

namespace GestorPacienteApi.DTOs.ExamenDto
{
    public class PostExamenDto
    {
        public int IdPrueba { get; set; }
        public int IdPaciente { get; set; }
    }
}
