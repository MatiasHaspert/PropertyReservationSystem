using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyReservation.Domain.Entities
{
    public class PropertyAvailability
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PropertyId { get; set; }
        
        public Property Property { get; set; } = null!;
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}
