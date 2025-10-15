using PropertyReservation.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyReservation.Application.DTOs.Property
{
    public class PropertyRequestDTO
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Title { get; set; } = string.Empty;
        [MinLength(0, ErrorMessage = "La descripción debe tener al menos 0 caracteres.")]
        [MaxLength(3000, ErrorMessage = "La descripción no puede exceder los 3000 caracteres.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "El precio por noche es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio por noche debe ser mayor que cero.")]
        public decimal NightlyPrice { get; set; }
        [Required(ErrorMessage = "La capacidad de huéspedes es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad de huéspedes debe ser al menos 1.")]
        public int MaxGuests { get; set; }
        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        public AddressDTO Address { get; set; } = new AddressDTO();

        // Luego vemos como manejar los Amenities
    }
}
