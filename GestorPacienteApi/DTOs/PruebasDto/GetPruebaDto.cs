using System.ComponentModel.DataAnnotations;

namespace GestorPacienteApi.DTOs.PruebasDto
{
    public class GetPruebaDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }
}
