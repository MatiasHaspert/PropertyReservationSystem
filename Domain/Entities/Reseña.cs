using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaPropiedades.Domain.Entities
{
    public class Reseña
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Calificacion { get; set; } // Calificación del 1 al 5
        public string Comentario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int PropiedadId { get; set; }
        public Propiedad Propiedad { get; set; } = null!;
        //public int UsuarioId { get; set; }
        //public Usuario Usuario { get; set; }
    }
}
