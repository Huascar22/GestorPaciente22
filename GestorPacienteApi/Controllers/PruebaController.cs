using AutoMapper;
using GestorPacienteApi.DTOs.CitasDtos;
using GestorPacienteApi.DTOs.PruebasDto;
using GestorPacienteApi.DTOs.UsuariosDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Modelo.Interfaces;

namespace GestorPacienteApi.Controllers
{
    [ApiController]
    [Route("Api/Prueba")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PruebaController: ControllerBase
    {
        private readonly IServicios<Prueba> _servicios;
        private readonly CancellationToken _token;
        private readonly IMapper _mapper;
        public PruebaController(IServicios<Prueba> _servicios, IMapper mapper)
        {
            this._servicios = _servicios;
            this._mapper = mapper;
        }

        [HttpGet(Name = "ObtenerPruebas")]
        public async Task<IActionResult> Get()
        {
            List<Prueba> pruebas = await _servicios.Obtener(_token);
            if (pruebas.Count == 0) return NoContent();
            List<GetPruebaDto> citasDto = _mapper.Map<List<GetPruebaDto>>(pruebas);
            return Ok(citasDto);
        }

        [HttpGet("Id", Name = "ObtenerPruebasById")]
        public async Task<ActionResult<GetPruebaDto>> Get(int Id)
        {
            Prueba prueba = new();
            prueba = await _servicios.ObtenerById(Id, prueba, _token);
            if (prueba.Id == 0) return NotFound($"No hay Prueba con el id {Id}");
            GetPruebaDto citaDto = _mapper.Map<GetPruebaDto>(prueba);
            return Ok(citaDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostPruebaDto pruebaDto)
        {
            if (!ModelState.IsValid) return BadRequest(pruebaDto);


            Prueba prueba = _mapper.Map<Prueba>(pruebaDto);
            Prueba pruebaNew = await _servicios.Crear(prueba, _token);

            return CreatedAtRoute("ObtenerPruebas", _mapper.Map<GetPruebaDto>(pruebaNew));
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(int id)
        {
            Prueba pruebaBorrar = new();
            List<Prueba> pruebas = await _servicios.Obtener(_token);
            var existePrueba = pruebas.Any(P => P.Id == id);
            if (!existePrueba) return BadRequest($"No hay prueba con el id {id}");
            pruebaBorrar = await _servicios.ObtenerById(id, pruebaBorrar, _token);
            await _servicios.Borrar(pruebaBorrar, _token);
            return NoContent();
        }
    }
}
