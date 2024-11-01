using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Modelo.Entidades;
using Modelo.Interfaces;
using System.Threading;
using Vista.CargarArchivos;
using Vista.ViewModels;

namespace Vista.Controllers
{
    [Authorize]
    public class PacienteController : Controller
    {
        private readonly IServicios<Paciente> _servicios;
        private readonly CancellationToken _token;
        private readonly IMapper _mapper;
        public PacienteController(IServicios<Paciente> _servicios,
            IMapper _mapper)
        {
            this._servicios = _servicios;
            this._mapper = _mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<Paciente> pacientes = await _servicios.Obtener(_token);
            return View(pacientes);
        }
        public IActionResult Crear() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(PacienteViewModel paciente)
        {
            if(!ModelState.IsValid) return View(paciente);
            Paciente pacienteSave = _mapper.Map<Paciente>(paciente);
            Paciente pacienteNew = await _servicios.Crear(pacienteSave, _token);
            pacienteNew.Foto = Archivos.UploadFile(paciente.File, pacienteNew.Id, "Paciente");
            await _servicios.Actualizar(pacienteNew, _token);
            return LocalRedirect("~/Paciente/Index");
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int id)
        {
            Paciente pacienteBorrar = new();
            pacienteBorrar = await _servicios.ObtenerById(id, pacienteBorrar, _token);
            await _servicios.Borrar(pacienteBorrar, _token);
            return LocalRedirect("~/Paciente/Index");
        }
        public async Task<IActionResult> Editar(int id)
        {
            Paciente pacienteEditar = new();
            pacienteEditar = await _servicios.ObtenerById(id, pacienteEditar, _token);
            PacienteViewModel viewModel = _mapper.Map<PacienteViewModel>(pacienteEditar);
            viewModel.Foto = pacienteEditar.Foto;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(PacienteViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            Paciente pacienteEditar = _mapper.Map<Paciente>(model);
            if (model.File != null) pacienteEditar.Foto = Archivos.UploadFile(model.File, model.Id, "Paciente");
            else pacienteEditar.Foto = model.Foto;
            await _servicios.Actualizar(pacienteEditar, _token);
            return LocalRedirect("~/Paciente/Index");
        }
    }
}
