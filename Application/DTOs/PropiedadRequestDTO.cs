using ReservaPropiedades.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaPropiedades.Application.DTOs
{
    public class PropiedadRequestDTO
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Titulo { get; set; } = string.Empty;
        [MinLength(0, ErrorMessage = "La descripción debe tener al menos 0 caracteres.")]
        [MaxLength(3000, ErrorMessage = "La descripción no puede exceder los 3000 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;
        [Required(ErrorMessage = "El precio por noche es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio por noche debe ser mayor que cero.")]
        public decimal PrecioPorNoche { get; set; }
        [Required(ErrorMessage = "La capacidad de huéspedes es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad de huéspedes debe ser al menos 1.")]
        public int CapacidadHuespedes { get; set; }
        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        public UbicacionDTO Ubicacion { get; set; } = new UbicacionDTO();

        // Luego vemos como manejar los servicios
    }
}
