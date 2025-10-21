using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Backend.Domain.ValueObjects;

namespace Backend.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public Address? Address { get; set; }
        
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;
        // Roles
        // Investigar cómo vincularlo con el campo de las properties y reactivar
        // public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
