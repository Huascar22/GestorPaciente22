using GestorPacienteApi.DTOs.LoginDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestorPacienteApi.Controllers
{
    [ApiController]
    [Route("Api/Login")]
    public class LoginController : ControllerBase
    {
        string resetPassword = "Reseteo de Password";
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration configuration;

        public LoginController(SignInManager<IdentityUser> _signInManager,
                UserManager<IdentityUser> _userManager,
                IConfiguration configuration)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<GetLoginDto>> Post(PostLoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var usuario = await _userManager.FindByNameAsync(model.Username);
            if (usuario is not null)
            {
                var resultado = await _signInManager.PasswordSignInAsync(usuario, model.Password, false, true);
                if (resultado.Succeeded) return await ConstruirToken(model);
                if (resultado.IsLockedOut) return BadRequest("Este usuario esta bloqueado");
            }
            ModelState.AddModelError(string.Empty, "Usuario O Clave Incorrecta");
            return BadRequest(ModelState);
        }





        private async Task<GetLoginDto> ConstruirToken(PostLoginDto credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("Username", credencialesUsuario.Username),
            };

            var usuario = await _userManager.FindByNameAsync(credencialesUsuario.Username);
            var claimsDB = await _userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new GetLoginDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }
    }
}
