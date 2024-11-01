using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Vista.Views.Shared.Servicios{
    public class ServiciosView:Controller{
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ServiciosView(SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager){
            this._signInManager = _signInManager;
            this._userManager = _userManager;
        }

        [Authorize]
        public async Task<IActionResult> ObtenerUsuarios(){
            IdentityUser? userActual = await _userManager.GetUserAsync(User);
            var users = _userManager.Users.ToList();
            users = users.Where(x => x.UserName != userActual.UserName).ToList();
            TempData["usuarios"] = JsonConvert.SerializeObject(users);
            return LocalRedirect("~/Home/Index");
        }
        public void ValidarError(IdentityResult result){
            foreach (var error in result.Errors){
                if (error.Code == "DuplicateUserName"){
                    ModelState.AddModelError(string.Empty, "El nombre de usuario ya existe..");
                    break;
                }ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
