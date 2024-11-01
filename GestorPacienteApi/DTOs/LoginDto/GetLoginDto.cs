namespace GestorPacienteApi.DTOs.LoginDto
{
    public class GetLoginDto
    {
        public string? Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
