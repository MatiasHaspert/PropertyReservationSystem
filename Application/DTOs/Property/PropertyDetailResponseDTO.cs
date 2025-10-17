using PropertyReservation.Application.DTOs.Amenity;
using PropertyReservation.Application.DTOs.PropertyImage;
using PropertyReservation.Application.DTOs.Review;

namespace PropertyReservation.Application.DTOs.Property
{
    public class PropertyDetailResponseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal NightlyPrice { get; set; }
        public int MaxGuests { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public AddressDTO Address { get; set; } = new AddressDTO();
        public decimal AverageRating { get; set; } // Calificación promedio de reseñas
        // Luego UserDTO, anfitrion
        //public UserDTO Anfitrion { get; set; } = new UserDTO();
        public List<AmenityResponseDTO> Amenities { get; set; } = new List<AmenityResponseDTO>();
        public List<PropertyImageResponseDTO> Images { get; set; } = new List<PropertyImageResponseDTO>();
        public List<ReviewResponseDTO> Reviews { get; set; } = new List<ReviewResponseDTO>();
    }
}
