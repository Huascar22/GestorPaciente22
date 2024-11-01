using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Vista.Views.Shared.Servicios;
namespace Vista.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ServiciosView _servicioView;
        public UsuarioController(IMapper _mapper, SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, ServiciosView _servicioView)
        {
            this._mapper = _mapper;
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._servicioView = _servicioView;
        }
        public async Task<IActionResult> Editar(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return View("Editar", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Usuario model, CancellationToken token)
        {
            Usuario? user = (Usuario?)await _userManager.FindByIdAsync(model.Id);
            user.Nombre = model.Nombre;
            user.TipoUsuario = model.TipoUsuario;
            user.Email = model.Email;
            user.Apellido = model.Apellido;
            user.UserName = model.UserName;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return RedirectToAction("ObtenerUsuarios", "ServiciosView");
            _servicioView.ValidarError(result); return View(model);
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("ObtenerUsuarios", "ServiciosView");
        }
    }
}
