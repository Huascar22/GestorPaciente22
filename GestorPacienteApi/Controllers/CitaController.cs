using AutoMapper;
using GestorPacienteApi.DTOs.CitasDtos;
using GestorPacienteApi.DTOs.MedicosDto;
using GestorPacienteApi.DTOs.UsuariosDtos;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Interfaces;
using System.Threading;

namespace GestorPacienteApi.Controllers
{
    [ApiController]
    [Route("Api/Cita")]
    public class CitaController: ControllerBase
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

        [HttpGet(Name = "ObtenerCitas")]
        public async Task<IActionResult> Get()
        {
            List<Cita> citas = await _servicios.Obtener(_token);
            if (citas.Count == 0) return NoContent();
            List<GetCitaDto> citasDto = _mapper.Map<List<GetCitaDto>>(citas);
            return Ok(citasDto);
        }

        [HttpGet("Id", Name = "ObtenerCitasById")]
        public async Task<ActionResult<GetUsuarioDto>> Get(int Id)
        {
            Cita cita = new();
            cita = await _servicios.ObtenerById(Id, cita, _token);
            if (cita.Id == 0) return NotFound($"No hay Cita con el id {Id}");
            GetCitaDto citaDto = _mapper.Map<GetCitaDto>(cita);
            return Ok(citaDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostCitaDto citadto)
        {
            if (!ModelState.IsValid) return BadRequest(citadto);

            List<Paciente> pacientes = await _paciente.Obtener(_token);
            var existePaciente = pacientes.Any(P => P.Id == citadto.PacienteId);
            if(!existePaciente) return BadRequest($"No hay paciente con el id {citadto.PacienteId}");

            List<Medico> medicos = await _medico.Obtener(_token);
            var existeMedico = medicos.Any(P => P.Id == citadto.MedicoId);
            if (!existeMedico) return BadRequest($"No hay Medico con el id {citadto.MedicoId}");

            Cita cita = _mapper.Map<Cita>(citadto);
            Cita citanew = await _servicios.Crear(cita, _token);

            return CreatedAtRoute("ObtenerCitas", _mapper.Map<GetCitaDto>(citanew));
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(int id)
        {
            Cita citaBorrar = new();
            List<Cita> citas = await _servicios.Obtener(_token);
            var existeCita = citas.Any(P => P.Id == id);    
            if(!existeCita) return BadRequest($"No hay cita con el id {id}");
            citaBorrar = await _servicios.ObtenerById(id, citaBorrar, _token);
            await _servicios.Borrar(citaBorrar, _token);
            return NoContent();
        }
    }
}
