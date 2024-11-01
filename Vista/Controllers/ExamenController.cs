using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Interfaces;
using Vista.ViewModels;
using Modelo.Enums;
using System.Collections.Generic;

namespace Vista.Controllers
{
    [Authorize]
    public class ExamenController : Controller
    {
        private readonly IServicios<ExamenMedico> _servicios;
        private readonly IServicios<Prueba> _serviciosPrueba;
        private readonly IServicios<Paciente> _serviciosPaciente;
        private readonly IMapper mapper;
        private readonly CancellationToken _token;

        public ExamenController(IServicios<ExamenMedico> _servicios, 
            IMapper mapper, IServicios<Prueba> _serviciosPrueba
            , IServicios<Paciente> _serviciosPacient)
        {
            this._servicios = _servicios;
            this.mapper = mapper;
            this._serviciosPaciente = _serviciosPacient;
            this._serviciosPrueba = _serviciosPrueba;
        }
        public IActionResult Index()
        {
            List<ExamenMedico> examenes =  _servicios
                .GetWithInclude(E => ((ExamenMedico)E).Paciente, 
                E => ((ExamenMedico)E).Prueba).ToList();
            examenes = examenes.Where(E => E.EstadoResultado == EstadoResultado.Pendiente).ToList();
            return View(examenes);
        }

        public async Task<IActionResult> Crear()
        {
            ExamenViewModel examen = new();            
            examen.Pruebas = await _serviciosPrueba.Obtener(_token);
            examen.Pacientes = await _serviciosPaciente.Obtener(_token);
            return View(examen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ExamenViewModel model)
        {
            ExamenMedico examenMedico = new();
            examenMedico.IdPrueba = model.SelectedPruebaId;
            examenMedico.IdPaciente = model.SelectedPacienteId;
            examenMedico.EstadoResultado = EstadoResultado.Pendiente;
            await _servicios.Crear(examenMedico, _token);
            return LocalRedirect("~/Examen/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearResultado(int Id, string Comentario)
        {
            ExamenMedico examen = new();
            examen = await _servicios.ObtenerById(Id, examen, _token);
            examen.Resultado = Comentario;
            examen.EstadoResultado = EstadoResultado.Completado;
            await _servicios.Actualizar(examen, _token);
            return LocalRedirect("~/Examen/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuscarByCedula(string Cedula)
        {
            List <ExamenMedico> examenes = _servicios
                .GetWithInclude(E => ((ExamenMedico)E).Paciente,
                E => ((ExamenMedico)E).Prueba).ToList();
            examenes = examenes.Where(E => E.Paciente.Cedula == Cedula).ToList();
            if (examenes.Count == 0) return View("CedulaNotFound");
            return View("Index", examenes);
        }
    }
}
