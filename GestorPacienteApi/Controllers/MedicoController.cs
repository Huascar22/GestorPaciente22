using AutoMapper;
using GestorPacienteApi.DTOs.MedicosDto;
using GestorPacienteApi.DTOs.UsuariosDtos;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Modelo.AppContext;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GestorPacienteApi.Controllers
{
    [ApiController]
    [Route("Api/Medico")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MedicoController : ControllerBase
    {
        private readonly IServicios<Medico> _servicios;
        private readonly IMapper _mapper;
        private readonly CancellationToken _cancellationToken;
        private readonly ContextoApp _contextoApp;
        public MedicoController(IServicios<Medico> _servicios,
            IMapper _mappe, ContextoApp _contextoApp)
        {
            this._servicios = _servicios;
            _mapper = _mappe;
            this._contextoApp = _contextoApp;

        }

        [HttpGet(Name = "ObtenerMedicos")]
        public async Task<IActionResult> Get()
        {
            List<Medico> medicos = await _servicios.Obtener(_cancellationToken);
            if (medicos.Count == 0) return NoContent();
            List<GetMedicoDto> medicosDtos = _mapper.Map<List<GetMedicoDto>>(medicos);
            return Ok(medicosDtos);
        }

        [HttpGet("Id", Name = "ObtenerMedicosById")]
        public async Task<ActionResult<GetMedicoDto>> Get(int Id)
        {
            Medico medico = new();
            medico = await _servicios.ObtenerById(Id, medico, _cancellationToken);
            
            if (medico.Id == 0) return NotFound($"No hay Medico con el id {Id}");
            GetMedicoDto getMedicoDto = _mapper.Map<GetMedicoDto>(medico);
            return Ok(getMedicoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostMedicoDto medicoVM)
        {
            if (!ModelState.IsValid) return BadRequest(medicoVM);
            Medico medicoSave = _mapper.Map<Medico>(medicoVM);
            Medico medicoNew = await _servicios.Crear(medicoSave, _cancellationToken);
            await _servicios.Actualizar(medicoNew, _cancellationToken);
            return CreatedAtRoute("ObtenerMedicos", _mapper.Map<GetMedicoDto>(medicoNew));
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(int id)
        {
            Medico medicoBorrar = new();

            List<Medico> citas = await _servicios.Obtener(_cancellationToken);
            var existeMedico = citas.Any(P => P.Id == id);
            if (!existeMedico) return BadRequest($"No hay medico con el id {id}");

            medicoBorrar = await _servicios.ObtenerById(id, medicoBorrar, _cancellationToken);
            await _servicios.Borrar(medicoBorrar, _cancellationToken);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put(PatchMedicoDto model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            Medico medicoEditar = _mapper.Map<Medico>(model);
            await _servicios.Actualizar(medicoEditar, _cancellationToken);
            return Ok(_mapper.Map<GetMedicoDto>(medicoEditar));
        }
    }
}
