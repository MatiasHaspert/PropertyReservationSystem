using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs.PropertyAvailability
{
    public class PropertyAvailabilityRequestDTO
    {
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "El ID de la propiedad es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la propiedad debe ser válido.")]
        public int PropertyId { get; set; }
    }
}
