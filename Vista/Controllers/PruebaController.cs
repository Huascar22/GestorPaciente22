using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Interfaces;
using System.Threading;
using Vista.CargarArchivos;
using Vista.ViewModels;

namespace Vista.Controllers{
    [Authorize]
    public class PruebaController : Controller{
        private readonly IServicios<Prueba> _servicios;
        private readonly CancellationToken _token;
        private readonly IMapper mapper;
        private readonly CancellationToken _cancellationToken;
        public PruebaController(IServicios<Prueba> _servicios, IMapper mapper)
        {
            this._servicios = _servicios;
            this.mapper = mapper;
        }
        [Authorize]
        public async Task<ActionResult> Index(){
            PruebaViewModel pruebaViewModel = new();
            pruebaViewModel.Pruebas = await _servicios.Obtener(_token);
            return View(pruebaViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(PruebaViewModelFormulario prueba){
            if (!ModelState.IsValid) {
                PruebaViewModel model = new PruebaViewModel();
                model.PruebaForomulario = prueba;
                model.Pruebas = await _servicios.Obtener(_token);
                return View("Index", model);
            }       
            await _servicios.Crear(mapper.Map<Prueba>(prueba), _token);
            return LocalRedirect("~/Prueba/Index");
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int id)
        {
            Prueba pruebaBorrar = new();
            pruebaBorrar = await _servicios.ObtenerById(id, pruebaBorrar, _cancellationToken);
            await _servicios.Borrar(pruebaBorrar, _cancellationToken);
            return LocalRedirect("~/Prueba/Index");
        }
        public async Task<IActionResult> Editar(int id)
        {
            Prueba pruebaBorrar = new();
            pruebaBorrar = await _servicios.ObtenerById(id, pruebaBorrar, _cancellationToken); ;
            return View(pruebaBorrar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Prueba model)
        {
            if (!ModelState.IsValid) return View(model);
            await _servicios.Actualizar(model, _cancellationToken);
            return LocalRedirect("~/Prueba/Index");
        }
    }
}
