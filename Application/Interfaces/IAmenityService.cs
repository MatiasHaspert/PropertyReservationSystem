using PropertyReservation.Application.DTOs.Amenity;
using PropertyReservation.Domain.Entities;

namespace PropertyReservation.Application.Interfaces
{
    public interface IAmenityService
    {
        Task<IEnumerable<AmenityResponseDTO>> GetAllAmenitiesAsync();
        Task<AmenityResponseDTO> CreateAmenityAsync(AmenityRequestDTO amenityRequestDTO);
        Task<bool> UpdateAmenityAsync(int amenityId, AmenityRequestDTO amenityRequestDTO);
        Task<bool> DeleteAmenityAsync(int amenityId);
    }
}
