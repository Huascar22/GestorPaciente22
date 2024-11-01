using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Newtonsoft.Json;

namespace Vista.Controllers{
    [Authorize]
    public class HomeController : Controller{
        public IActionResult Index(){
            if (TempData["usuarios"] is not null){
                var usuarios = JsonConvert.
                    DeserializeObject<List<Usuario>>(TempData["usuarios"].ToString());
                return View(usuarios);
            }return View();
        }
        public IActionResult AccesoInvalido() => View();
        public IActionResult RolInvalido() => View();
    }
}
