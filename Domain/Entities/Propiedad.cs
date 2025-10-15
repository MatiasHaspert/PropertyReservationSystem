using Microsoft.EntityFrameworkCore;
using ReservaPropiedades.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaPropiedades.Domain.Entities
{
    public class Propiedad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        [Precision(18, 2)]
        public decimal PrecioPorNoche { get; set; }
        [Required]
        public int CapacidadHuespedes { get; set; }
        [Required]
        public string Titulo { get; set; } = string.Empty;
        public int NumeroHabitaciones { get; set; }
        public int NumeroBaños { get; set; }
        [Required]
        public Ubicacion Ubicacion { get; set; } = new Ubicacion();
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
        public ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();
        public ICollection<ImagenPropiedad> Imagenes { get; set; } = new List<ImagenPropiedad>();
        public ICollection<DisponibilidadPropiedad> Disponibilidades { get; set; } = new List<DisponibilidadPropiedad>();

    }
}
