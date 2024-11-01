using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vista.ViewModels;
using Vista.Views.Shared.Servicios;

namespace Vista.Controllers{
    public class LoginController : Controller{
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ServiciosView _servicioView;
        private readonly RoleManager<IdentityRole> _roleManager;
        public LoginController(SignInManager<IdentityUser> _signInManager,
                UserManager<IdentityUser> _userManager,
                ServiciosView _servicioView,
                RoleManager<IdentityRole> _roleManager)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._servicioView = _servicioView;
            this._roleManager = _roleManager;
        }
        public async Task<IActionResult> Login(){
            LoginViewModel vm = new LoginViewModel();
            await _signInManager.SignOutAsync();

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model){
            if (!ModelState.IsValid) return View(model);
            var usuario = await _userManager.FindByNameAsync(model.Username);
            if (usuario is not null){
                var resultado = await _signInManager.PasswordSignInAsync(usuario, model.Password, model.Recordar, true);
                if (resultado.Succeeded) return RedirectToAction("ObtenerUsuarios", "ServiciosView");
                if (resultado.IsLockedOut) model.AcessoDenegado = true;
            }ModelState.AddModelError(string.Empty, "Usuario O Clave Incorrecta");
            return View(model);
        }


       
    }
}
