using PropertyReservation.Application.DTOs.Amenity;
using PropertyReservation.Domain.Entities;

namespace PropertyReservation.Application.Interfaces
{
    public interface IAmenityService
    {
        Task<IEnumerable<AmenityResponseDTO>> GetAllAmenitiesAsync();
        Task<AmenityResponseDTO> CreateAmenityAsync(AmenityRequestDTO amenityRequestDTO);
        Task UpdateAmenityAsync(int amenityId, AmenityRequestDTO amenityRequestDTO);
        Task DeleteAmenityAsync(int amenityId);
    }
}
