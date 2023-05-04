using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Models.Dtos
{
    public class UsuarioDTO
    {

        [Required(ErrorMessage = "El username es obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; }
    }
}
