using Microsoft.AspNetCore.Mvc;
using Backend_Final.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Backend_Final.RepositoryPattern.IRepository;
using Backend_Final.Models.Dtos;
using Backend_Final.RepositoryPattern;

namespace Backend_Final.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly IMapper _mapper;
        private IConfiguration _config;

        public UserController(IUsuarioRepository UsuarioRepository, IMapper mapper, IConfiguration config)
        {
            _UsuarioRepository = UsuarioRepository;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201, Type = typeof(UsuarioDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearUsuario([FromBody] UsuarioDTO crearUsuarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (crearUsuarioDto == null)
                return BadRequest(ModelState);

            if (_UsuarioRepository.ExisteUsuario(crearUsuarioDto.UserName))
            {
                ModelState.AddModelError("", "El ya existe");
                return StatusCode(404, ModelState);
            }

            var usuario = _mapper.Map<usuario>(crearUsuarioDto);
            if (!_UsuarioRepository.CrearUsuario(usuario))
            {
                ModelState.AddModelError("", $"Algo Salio mal guardando el registro {usuario.UserName}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetUsuario", new { usuarioId = usuario.Id }, usuario);
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        [ProducesResponseType(201, Type = typeof(UsuarioDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LoginUsuario([FromBody] UsuarioDTO LoginUsuarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (LoginUsuarioDto == null)
                return BadRequest(ModelState);

            var itemUsuario = _UsuarioRepository.GetUsuario(LoginUsuarioDto.UserName);

            if (itemUsuario == null)
                return NotFound();

            IActionResult response = Unauthorized();

            if (itemUsuario.UserName.ToLower().Trim() == LoginUsuarioDto.UserName.ToLower().Trim() && itemUsuario.Password.ToLower().Trim() == LoginUsuarioDto.Password.ToLower().Trim())
            {
                var token = GenerateToken(itemUsuario);
                response = Ok(new { token = token });
            }

            return response;
        }

        private string GenerateToken( usuario User) 
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
