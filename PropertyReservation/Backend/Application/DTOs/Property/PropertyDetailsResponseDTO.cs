using Backend.Application.DTOs.Amenity;
using Backend.Application.DTOs.PropertyImage;
using Backend.Application.DTOs.Review;

namespace Backend.Application.DTOs.Property
{
    public class PropertyDetailsResponseDTO
    {
        public int Id { get; set; }
        
        public string Title { get; set; } = string.Empty;
        
        public decimal NightlyPrice { get; set; }
        
        public int MaxGuests { get; set; }        
        
        public int Bedrooms { get; set; }
        
        public int Bathrooms { get; set; }
        
        public AddressDTO Address { get; set; } = new AddressDTO();
        
        public string Description { get; set; } = string.Empty;
        
        public decimal AverageRating { get; set; } 
        
        public List<AmenityResponseDTO> Amenities { get; set; } = new List<AmenityResponseDTO>();
        
        public List<PropertyImageResponseDTO> Images { get; set; } = new List<PropertyImageResponseDTO>();
        
        public List<ReviewResponseDTO> Reviews { get; set; } = new List<ReviewResponseDTO>();
    }
}
