using GestorPacienteApi.DTOs.LoginDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestorPacienteApi.Controllers
{
    [ApiController]
    [Route("Api/Login")]
    public class LoginController:ControllerBase
    {
        string resetPassword = "Reseteo de Password";
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;      
        public LoginController(SignInManager<IdentityUser> _signInManager,
                UserManager<IdentityUser> _userManager)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostLoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var usuario = await _userManager.FindByNameAsync(model.Username);
            if (usuario is not null)
            {
                var resultado = await _signInManager.PasswordSignInAsync(usuario, model.Password,false, true);
                if (resultado.Succeeded) return Ok();
                if (resultado.IsLockedOut) return BadRequest("Este usuario esta bloqueado");
            }
            ModelState.AddModelError(string.Empty, "Usuario O Clave Incorrecta");
            return BadRequest(ModelState); 
        }
    }
}
