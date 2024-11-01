using AutoMapper;
using GestorPacienteApi.DTOs.MedicosDto;
using GestorPacienteApi.DTOs.PacientesDtos;
using GestorPacienteApi.DTOs.UsuariosDtos;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Interfaces;
using System.Threading;

namespace GestorPacienteApi.Controllers
{
    [ApiController]
    [Route("Api/Paciente")]
    public class PacienteController: ControllerBase
    {
        private readonly IServicios<Paciente> _servicios;
        private readonly CancellationToken _cancellationToken;
        private readonly IMapper _mapper;
        public PacienteController(IServicios<Paciente> _servicios,
            IMapper _mapper)
        {
            this._servicios = _servicios;
            this._mapper = _mapper;
        }

        [HttpGet(Name = "ObtenerPacientes")]
        public async Task<IActionResult> Get()
        {
            List<Paciente> pacientes = await _servicios.Obtener(_cancellationToken);
            if (pacientes.Count == 0) return NoContent();
            List<GetPacienteDto> medicosDtos = _mapper.Map<List<GetPacienteDto>>(pacientes);
            return Ok(medicosDtos);
        }

        [HttpGet("Id", Name = "ObtenerPacientesById")]
        public async Task<ActionResult<GetPacienteDto>> Get(int Id)
        {
            Paciente pacientes = new();
            pacientes = await _servicios.ObtenerById(Id, pacientes, _cancellationToken);

            if (pacientes.Id == 0) return NotFound($"No hay paciente con el id {Id}");
            GetPacienteDto getPacienteDto = _mapper.Map<GetPacienteDto>(pacientes);
            return Ok(getPacienteDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostPacienteDto medicoVM)
        {
            if (!ModelState.IsValid) return BadRequest(medicoVM);
            Paciente pacienteSave = _mapper.Map<Paciente>(medicoVM);
            Paciente pacienteNew = await _servicios.Crear(pacienteSave, _cancellationToken);
            await _servicios.Actualizar(pacienteNew, _cancellationToken);
            return CreatedAtRoute("ObtenerPacientes", _mapper.Map<GetPacienteDto>(pacienteNew));
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(int id)
        {
            Paciente oacienteBorrar = new();

            List<Paciente> citas = await _servicios.Obtener(_cancellationToken);
            var existePaciente = citas.Any(P => P.Id == id);
            if (!existePaciente) return BadRequest($"No hay paciente con el id {id}");

            oacienteBorrar = await _servicios.ObtenerById(id, oacienteBorrar, _cancellationToken);
            await _servicios.Borrar(oacienteBorrar, _cancellationToken);
            return NoContent();
        }
    }
}
