using AutoMapper;
using GestorPacienteApi.DTOs.CitasDtos;
using GestorPacienteApi.DTOs.ExamenDto;
using GestorPacienteApi.DTOs.UsuariosDtos;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Interfaces;

namespace GestorPacienteApi.Controllers
{
    [ApiController]
    [Route("Api/Examen")]
    public class ExamenController: ControllerBase
    {
        private readonly IServicios<ExamenMedico> _servicios;
        private readonly IServicios<Prueba> _serviciosPrueba;
        private readonly IServicios<Paciente> _serviciosPaciente;
        private readonly IMapper _mapper;
        private readonly CancellationToken _token;

        public ExamenController(IServicios<ExamenMedico> _servicios,
            IMapper mapper, IServicios<Prueba> _serviciosPrueba
            , IServicios<Paciente> _serviciosPacient)
        {
            this._servicios = _servicios;
            this._mapper = mapper;
            this._serviciosPaciente = _serviciosPacient;
            this._serviciosPrueba = _serviciosPrueba;
        }

        [HttpGet(Name = "ObtenerExamenes")]
        public async Task<IActionResult> Get()
        {
            List<ExamenMedico> examenes = await _servicios.Obtener(_token);
            if (examenes.Count == 0) return NoContent();
            List<GetExamenDto> citasDto = _mapper.Map<List<GetExamenDto>>(examenes);
            return Ok(citasDto);
        }

        [HttpGet("Id", Name = "ObtenerExamenesById")]
        public async Task<ActionResult<GetExamenDto>> Get(int Id)
        {
            ExamenMedico examen = new();
            examen = await _servicios.ObtenerById(Id, examen, _token);
            if (examen.Id == 0) return NotFound($"No hay examen con el id {Id}");
            GetExamenDto citaDto = _mapper.Map<GetExamenDto>(examen);
            return Ok(citaDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostExamenDto examenDto)
        {
            if (!ModelState.IsValid) return BadRequest(examenDto);

            List<Paciente> pacientes = await _serviciosPaciente.Obtener(_token);
            var existePaciente = pacientes.Any(P => P.Id == examenDto.IdPaciente);
            if (!existePaciente) return BadRequest($"No hay paciente con el id {examenDto.IdPaciente}");

            List<Prueba> pruebas = await _serviciosPrueba.Obtener(_token);
            var existePrueba = pruebas.Any(P => P.Id == examenDto.IdPrueba);
            if (!existePrueba) return BadRequest($"No hay Prueba con el id {examenDto.IdPrueba}");

            ExamenMedico examen = _mapper.Map<ExamenMedico>(examenDto);
            ExamenMedico examenNew = await _servicios.Crear(examen, _token);

            return CreatedAtRoute("ObtenerExamenes", _mapper.Map<GetExamenDto>(examenNew));
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(int id)
        {
            ExamenMedico examenMedico = new();
            List<ExamenMedico> examenes = await _servicios.Obtener(_token);
            var existeExamen = examenes.Any(P => P.Id == id);
            if (!existeExamen) return BadRequest($"No hay Examen con el id {id}");
            examenMedico = await _servicios.ObtenerById(id, examenMedico, _token);
            await _servicios.Borrar(examenMedico, _token);
            return NoContent();
        }
    }
}
