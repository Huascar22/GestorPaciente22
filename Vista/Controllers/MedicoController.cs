using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Interfaces;
using Vista.CargarArchivos;
using Vista.ViewModels;
namespace Vista.Controllers{

    [Authorize]
    public class MedicoController : Controller
    {
        private readonly IServicios<Medico> _servicios;
        private readonly IMapper _mapper;
        private readonly CancellationToken _cancellationToken;
        public MedicoController(IServicios<Medico> _servicios,
            IMapper _mappe)
        {
            this._servicios = _servicios;
            this._mapper = _mappe;
        }
        public async Task<IActionResult> Index()
        {
            List<Medico> medicos = await _servicios.Obtener(_cancellationToken);
            return View(medicos);
        }
        public IActionResult Crear() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(MedicoViewModel medicoVM)
        {
            if (!ModelState.IsValid) return View(medicoVM);
            Medico medicoSave = _mapper.Map<Medico>(medicoVM);
            Medico medicoNew = await _servicios.Crear(medicoSave, _cancellationToken);
            medicoNew.Foto = Archivos.UploadFile(medicoVM.File, medicoNew.Id, "Medico");
            await _servicios.Actualizar(medicoNew, _cancellationToken);
            return LocalRedirect("~/Medico/Index");
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int id)
        {
            Medico medicoBorrar = new();
            medicoBorrar = await _servicios.ObtenerById(id, medicoBorrar, _cancellationToken);
            await _servicios.Borrar(medicoBorrar, _cancellationToken);
            return LocalRedirect("~/Medico/Index");
        }
        public async Task<IActionResult> Editar(int id)
        {
            Medico medicoEditar = new();
            medicoEditar = await _servicios.ObtenerById(id, medicoEditar, _cancellationToken);
            MedicoViewModel viewModel = _mapper.Map<MedicoViewModel>(medicoEditar);
            viewModel.Foto = medicoEditar.Foto; 
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(MedicoViewModel model)
        {
            if(!ModelState.IsValid) return View(model);
            Medico medicoEditar = _mapper.Map<Medico>(model);
            if (model.File != null) medicoEditar.Foto = Archivos.UploadFile(model.File, model.Id, "Medico");
            else medicoEditar.Foto = model.Foto;
            await _servicios.Actualizar(medicoEditar, _cancellationToken);
            return LocalRedirect("~/Medico/Index");
        }
    }
}
