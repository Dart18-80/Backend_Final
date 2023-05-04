using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Final.Models
{
    public class bicicleta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Tamaño { get; set; }
        [Required]
        public string CantidadPlatos { get; set; }
        [Required]
        public string CantidadPinones { get; set; }
        public string Image { get; set; }
    }
}
