using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs.Amenity
{
    public class AmenityRequestDTO
    {
        [Required(ErrorMessage = "El nombre del servicio es requerido")]
        [StringLength(100, ErrorMessage = "El nombre del servicio no puede exceder los 100 caracteres")]
        [MinLength(2, ErrorMessage = "El nombre del servicio debe tener al menos 2 caracteres")]
        public string Name { get; set; } = string.Empty;
    }
}
