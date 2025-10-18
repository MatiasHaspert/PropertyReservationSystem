using Microsoft.EntityFrameworkCore;
using PropertyReservation.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyReservation.Domain.Entities
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [Required]
        [Precision(18, 2)]
        public decimal NightlyPrice { get; set; }
        [Required]
        public int MaxGuests { get; set; }
        [Required]
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        [Required]
        public Address Address { get; set; } = new Address();
        public string Description { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
        public ICollection<PropertyAvailability> Availabilities { get; set; } = new List<PropertyAvailability>();
    }
}
