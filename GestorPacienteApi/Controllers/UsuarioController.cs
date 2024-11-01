using AutoMapper;
using GestorPacienteApi.DTOs.UsuariosDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Enums;
using Modelo.Interfaces;

namespace GestorPacienteApi.Controllers
{
    [ApiController]
    [Route("Api/Usuario")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController : ControllerBase
    {
        private IServicios<Usuario> _servicios;
        private readonly CancellationToken _token;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        public UsuarioController(IServicios<Usuario> _servicios,
            SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, IMapper _mapper)
        {
            this._servicios = _servicios;
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._mapper = _mapper;
        }

        [HttpGet(Name = "ObtenerUsuarios")]
        
        public async Task<ActionResult<List<GetUsuarioDto>>> Get()
        {
            List<Usuario> usuarios = await _servicios.Obtener(_token);
            List<GetUsuarioDto> getUsuariosDto = _mapper.Map<List<GetUsuarioDto>>(usuarios);
            if (getUsuariosDto.Count == 0) return NotFound("No hay usuarios que mostar");
            return Ok(getUsuariosDto);
        }

        [HttpGet("Id", Name = "ObtenerUsuariosById")]
        public async Task<ActionResult<GetUsuarioDto>> Get(string Id)
        {
            Usuario usuario = new();
            usuario = await _servicios.ObtenerById(Id, usuario, _token);
           
            if (usuario.UserName == null) return NotFound($"No hay usuario con el Id {Id}");
            GetUsuarioDto getUsuariosDto = _mapper.Map<GetUsuarioDto>(usuario);
            return Ok(getUsuariosDto);
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(string Id)
        {
            var usuarios = _userManager.Users.ToList();
            var existeUsuario = usuarios.Any(U => U.Id == Id);
            if (!existeUsuario) return BadRequest($"No hay usuario con el id {Id}");

            var user = await _userManager.FindByIdAsync(Id);
            await _userManager.DeleteAsync(user);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostUsuarioDto Vm)
        {
            if (!ModelState.IsValid) return BadRequest();
            Usuario usuario = _mapper.Map<Usuario>(Vm);
            var resultado = await _userManager.CreateAsync(usuario, Vm.Password);
            if (resultado.Succeeded)
            {
                await AsignarRole(usuario);
                GetUsuarioDto getUsuario = _mapper.Map<GetUsuarioDto>(usuario);
                return CreatedAtRoute("ObtenerUsuarios", getUsuario);
            }
            foreach (var error in resultado.Errors)
            {
                if (error.Code == "DuplicateUserName")
                {
                    ModelState.AddModelError(string.Empty, "El nombre de usuario ya existe..");
                    break;
                }
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
        private async Task AsignarRole(Usuario usuario)
        {
            if (usuario.TipoUsuario == TipoUsuario.Administrador)
                await _userManager.AddToRoleAsync(usuario, "Administrador");
            else await _userManager.AddToRoleAsync(usuario, "Asistente");
        }


    }
}
