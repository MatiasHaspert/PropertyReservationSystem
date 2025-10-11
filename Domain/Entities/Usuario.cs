using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ReservaPropiedades.Domain.ValueObjects;

namespace ReservaPropiedades.Domain.Entities
{
    // Usuario prueba 
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public Ubicacion Ubicacion { get; set; } = new Ubicacion();
        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;
        // Roles
        public ICollection<Propiedad> Propiedades { get; set; } = new List<Propiedad>();
        public ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();

    }
}
