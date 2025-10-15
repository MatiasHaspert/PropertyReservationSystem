using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyReservation.Domain.Entities
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Rating { get; set; } // Calificación del 1 al 5
        public string Comment { get; set; } = string.Empty;
        public DateTime Date { get; set; } // El campo Date lo generamos nosotros 
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
