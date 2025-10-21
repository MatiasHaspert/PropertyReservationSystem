using Microsoft.EntityFrameworkCore;
using Backend.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [Precision(18, 2)]
        public decimal NightlyPrice { get; set; }
        
        [Required]
        public int MaxGuests { get; set; }
        
        [Required]
        public int Bedrooms { get; set; }
        
        [Required]
        public int Bathrooms { get; set; }
        
        [Required]
        public Address? Address { get; set; }
        
        public string Description { get; set; } = string.Empty;
        
        public int OwnerId { get; set; }
        
        public User Owner { get; set; } = null!; // Navigation property (consultar porque no se establece automáticamente, hay alguna relación con el ownerId?)
        
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
        
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        
        public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
        
        public ICollection<PropertyAvailability> Availabilities { get; set; } = new List<PropertyAvailability>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
