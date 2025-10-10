using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaPropiedades.Domain.Entities
{
    public class ImagenPropiedad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; } = string.Empty;
        [Required]
        public string NombreArchivo { get; set; } = string.Empty;
        public bool EsImagenPrincipal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int PropiedadId { get; set; }
        public Propiedad Propiedad { get; set; } = null!;
    }
}
