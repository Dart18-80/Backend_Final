using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Models.Dtos
{
    public class BicicletaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El name es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        public string Type { get; set; }

        [Required(ErrorMessage = "La marca es obligatorio")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El tamaño es obligatorio")]
        public string Tamaño { get; set; }

        [Required(ErrorMessage = "La cantidad de Platos es obligatorio")]
        public int CantidadPlatos { get; set; }

        [Required(ErrorMessage = "La cantidad de piñones es obligatorio")]
        public int CantidadPinones { get; set; }
        public string Image { get; set; }
    }
}
