using System.ComponentModel.DataAnnotations;

namespace ReservaPropiedades.Application.DTOs
{
    public class UbicacionDTO
    {
        [Required(ErrorMessage = "La ciudad debe ser obligatoria")]
        [MaxLength(100, ErrorMessage = "La ciudad no puede exceder los 100 caracteres.")]
        public string Ciudad { get; set; } = string.Empty;
        [Required(ErrorMessage = "La provincia debe ser obligatorio")]
        [MaxLength(100, ErrorMessage = "La provincia no puede exceder los 100 caracteres.")]
        public string Provincia { get; set; } = string.Empty;
        [Required(ErrorMessage = "El país debe ser obligatorio")]
        [MaxLength(100, ErrorMessage = "El país no puede exceder los 100 caracteres.")]
        public string Pais { get; set; } = string.Empty;
        [Required(ErrorMessage = "La dirección debe ser obligatoria")]
        [MaxLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        public string Direccion { get; set; } = string.Empty;
        [Required(ErrorMessage = "El código postal debe ser obligatorio")]
        [Range(1, 99999, ErrorMessage = "El código postal debe estar entre 1 y 99999.")]
        public int CodigoPostal { get; set; }

        public UbicacionDTO() { }
    }
}
