using Microsoft.AspNetCore.Mvc;
using Backend_Final.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Backend_Final.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;

        public UserController( IConfiguration config)
        {
            _config = config;
        }

        private usuario AuthenticationUser (usuario User) 
        {
            usuario _user = null;
            if( User.UserName == "Diego" && User.Password == "Test123") 
            {
                _user = new usuario { UserName = "Diego Ruiz" };
            }
            return _user;
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(usuario user) 
        {
            IActionResult response = Unauthorized();
            var _user = AuthenticationUser(user);
            if(_user != null) 
            {
                var token = GenerateToken(user);
                response = Ok(new { token = token});
            }
            return response;
        }
    }
}
