using Backend.Application.DTOs.PropertyImage;

namespace Backend.Application.DTOs.Property
{
    public class PropertyListResponseDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal NightlyPrice { get; set; }

        public int MaxGuests { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public AddressDTO Address { get; set; } = new AddressDTO();

        public decimal AverageRating { get; set; } 

        public PropertyImageResponseDTO? MainImage { get; set; } 
    }
}
