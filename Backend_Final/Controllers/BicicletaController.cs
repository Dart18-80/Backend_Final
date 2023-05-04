using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend_Final.Models;
using Backend_Final.Models.Dtos;
using Backend_Final.RepositoryPattern.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Backend_Final.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/bicicleta")]
    public class BicicletaController : ControllerBase
    {
        private readonly IBicicletaRepository _bicicletaRepository;
        private readonly IMapper _mapper;

        public BicicletaController(IBicicletaRepository bicicletaRepository, IMapper mapper)
        {
            _bicicletaRepository = bicicletaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBicicleta()
        {
            var listaBicicleta = _bicicletaRepository._GetBicicleta();

            var listaBicicletaDto = new List<BicicletaDTO>();

            foreach (var item in listaBicicleta)
            {
                listaBicicletaDto.Add(_mapper.Map<BicicletaDTO>(item));
            }

            return Ok(listaBicicletaDto);
        }

        [HttpGet("{bicicletaId:int}", Name = "GetBicicleta")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBicicleta(int bicicletaId)
        {
            var itemBicicleta = _bicicletaRepository.GetBicicleta(bicicletaId);

            if (itemBicicleta == null)
                return NotFound();

            var itemBicicletaDto = _mapper.Map<BicicletaDTO>(itemBicicleta);

            return Ok(itemBicicletaDto);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(201, Type = typeof(BicicletaDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearBicicleta([FromBody] BicicletaDTO crearBicicletaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (crearBicicletaDto == null)
                return BadRequest(ModelState);

            if (_bicicletaRepository.ExisteBicicleta(crearBicicletaDto.Name))
            {
                ModelState.AddModelError("", "La Bicicleta ya existe");
                return StatusCode(404, ModelState);
            }

            var bicicleta = _mapper.Map<bicicleta>(crearBicicletaDto);
            if (!_bicicletaRepository.CrearBicicleta(bicicleta))
            {
                ModelState.AddModelError("", $"Algo Salio mal guardando el registro {bicicleta.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetBicicleta", new { bicicletaId = bicicleta.Id }, bicicleta);
        }

        [HttpPatch("{bicicletaId:int}", Name = "ActualizarPatchBicicleta")]
        [Authorize]
        [ProducesResponseType(201, Type = typeof(BicicletaDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchCity(int bicicletaId, [FromBody] BicicletaDTO bicicletaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (bicicletaDTO == null || bicicletaId != bicicletaDTO.Id)
                return BadRequest(ModelState);

            if (_bicicletaRepository.ExisteBicicleta(bicicletaDTO.Name))
            {
                ModelState.AddModelError("", "La bicicleta ya existe");
                return StatusCode(404, ModelState);
            }

            var bicicleta = _mapper.Map<bicicleta>(bicicletaDTO);

            if (!_bicicletaRepository.ActualizarBicicleta(bicicleta))
            {
                ModelState.AddModelError("", $"Algo Salio mal actualizando el registro {bicicleta.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{bicicletaId:int}", Name = "BorrarBicicleta")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarBicicleta(int bicicletaId)
        {
            if (!_bicicletaRepository.ExisteBicicleta(bicicletaId))
                return NotFound();

            var bicicleta = _bicicletaRepository.GetBicicleta(bicicletaId);

            if (!_bicicletaRepository.BorrarBicicleta(bicicleta))
            {
                ModelState.AddModelError("", $"Algo Salio mal Borrando el registro {bicicleta.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
