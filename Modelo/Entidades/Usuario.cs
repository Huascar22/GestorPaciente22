using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Modelo.Enums;

namespace Modelo.Entidades
{
    public class Usuario: IdentityUser
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public TipoUsuario? TipoUsuario { get; set; }
        
    }
}
