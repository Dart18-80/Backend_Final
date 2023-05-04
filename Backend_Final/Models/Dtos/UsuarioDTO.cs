using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Models.Dtos
{
    public class UsuarioDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
