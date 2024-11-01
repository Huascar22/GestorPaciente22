using Microsoft.AspNetCore.Identity;

namespace Vista.Controllers
{
    public class InicializarRoles
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public InicializarRoles(RoleManager<IdentityRole> _roleManager)
        {
            this._roleManager = _roleManager;
        }

        public async Task CrearRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));
            }
            if (!await _roleManager.RoleExistsAsync("Asistente"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Asistente"));
            }
        }
    }
}
