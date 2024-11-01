using AutoMapper;
using Controlador.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Vista.ViewModels;
using Vista.Views.Shared.Servicios;

namespace Vista.Controllers
{
    public class PasswordController : Controller
    {
        string resetPassword = "Reseteo de Password";
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ServiciosView _servicioView;
        public PasswordController(SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, IMapper _mapper, 
            ServiciosView _servicioView)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._mapper = _mapper;
            this._servicioView = _servicioView;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> RecuperarPassword(RecuperarClaveViewModel model){
            if (!ModelState.IsValid) return View(model);
            Usuario? usuario = (Usuario?)await _userManager.FindByEmailAsync(model.Correo);
            if (usuario == null){
                model.UsuarioNull = true;
                return View(model);
            }
            var codigo = await _userManager.GeneratePasswordResetTokenAsync(usuario);
            var urlRetorno = Url.Action("ResetPassword", "Password",
                            new { userId = usuario.Id, code = codigo, email = usuario.Email },
                            protocol: HttpContext.Request.Scheme);
            string mensaje = "Tocar el enlace para recuperar clave";
            EnviarCorreo.Enviar(model.Correo, resetPassword, mensaje, urlRetorno);
            return View("EnvioPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model){
            if (!ModelState.IsValid) return View(model);
            var usuario = await _userManager.FindByEmailAsync(model.Correo);
            var resultado = await _userManager.ResetPasswordAsync(usuario, model.Code, model.Password);
            if (resultado.Succeeded) return RedirectToAction("Login", "Login");
            _servicioView.ValidarError(resultado);
            return View(model);
        }

        public IActionResult RecuperarPassword(){
            RecuperarClaveViewModel model = new RecuperarClaveViewModel();
            return View(model);
        }
        public IActionResult EnvioPassword(){
            return View();
        }
        public IActionResult ResetPassword(string usuarioId, string code, string email)
        {
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel();
            resetPasswordViewModel.Correo = email;
            resetPasswordViewModel.Code = code;
            return View(resetPasswordViewModel);
        }
    }
}
