using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Vista.ViewModels;
using Vista.Views.Shared.Servicios;
using Modelo.Enums;

namespace Vista.Controllers
{
    public class RegistrarController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ServiciosView _servicioView;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrarController(SignInManager<IdentityUser> _signInManager,
                UserManager<IdentityUser> _userManager,
                ServiciosView _servicioView, IMapper _mapper,
                RoleManager<IdentityRole> _roleManager)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._servicioView = _servicioView;
            this._mapper = _mapper;
            this._roleManager = _roleManager;
        }
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroViewModel Vm)
        {
            if (!ModelState.IsValid) return View(Vm);
            Usuario usuario = _mapper.Map<Usuario>(Vm);
            var resultado = await _userManager.CreateAsync(usuario, Vm.Password);
            if (resultado.Succeeded)
            {
                await AsignarRole(usuario);
                if (User.Identity.IsAuthenticated) return RedirectToAction("ObtenerUsuarios", "ServiciosView");
                return RedirectToAction("Login", "Login");
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
            return View(Vm);
        }

        private async Task AsignarRole(Usuario usuario)
        {
            if (usuario.TipoUsuario == TipoUsuario.Administrador)
                await _userManager.AddToRoleAsync(usuario, "Administrador");
            else await _userManager.AddToRoleAsync(usuario, "Asistente");
        }
        
    }
}
