using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Interfaces;
using Vista.ViewModels;
using Modelo.Enums;

namespace Vista.Controllers
{
    [Authorize]
    public class CitaController : Controller
    {
        private readonly IServicios<Cita> _servicios;
        private readonly IServicios<Medico> _medico;
        private readonly IServicios<Paciente> _paciente;
        private readonly CancellationToken _token;
        private readonly IMapper _mapper;

        public CitaController(IServicios<Cita> _servicios, 
            IServicios<Medico> medico, IMapper _mapper,
            IServicios<Paciente> paciente)
        {
            this._servicios = _servicios;
            _medico = medico;
            this._mapper = _mapper;
            _paciente = paciente;
        }
        public IActionResult Index()
        {
            List<Cita> citas = _servicios
                .GetWithInclude(C => ((Cita)C).Paciente, 
               C => ((Cita)C).Medico).ToList();
            citas = citas.Where(C => C.EstadoCita == EstadoCita.PendienteConsulta).ToList();
            return View(citas);
        }

        public async Task<IActionResult> Crear()
        {
            CitaViewModel citaViewModel = new CitaViewModel();
            citaViewModel.Pacientes = await _paciente.Obtener(_token);
            citaViewModel.Medicos = await _medico.Obtener(_token);
            return View(citaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CitaViewModel model)
        {
            Cita cita = _mapper.Map<Cita>(model);
            await _servicios.Crear(cita, _token);
            return LocalRedirect("~/Cita/Index");
        }

        public async Task<IActionResult> CompletarCita(int Id)
        {
            Cita cita = new();
            cita = await _servicios.ObtenerById(Id, cita, _token);
            cita.EstadoCita = EstadoCita.Completada;
            await _servicios.Actualizar(cita, _token);
            return LocalRedirect("~/Cita/Index");
        }
    }
}
