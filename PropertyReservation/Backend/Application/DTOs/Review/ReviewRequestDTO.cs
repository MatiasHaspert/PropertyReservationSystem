using System.ComponentModel.DataAnnotations;

namespace Backend.Application.DTOs.Review
{
    public class ReviewRequestDTO
    {
        [Required(ErrorMessage = "La calificación es requerida")]
        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5")]
        public int Rating { get; set; } // Calificación del 1 al 5

        [Required(ErrorMessage = "El comentario es requerido")]
        [StringLength(1000, ErrorMessage = "El comentario no puede exceder los 1000 caracteres")]
        [MinLength(10, ErrorMessage = "El comentario debe tener al menos 10 caracteres")]
        public string Comment { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ID de la propiedad es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la propiedad debe ser un número válido")]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "El ID del usuario es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario debe ser un número válido")]
        public int UserId { get; set; }
    }
}
