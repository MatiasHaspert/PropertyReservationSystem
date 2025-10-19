using Backend.Application.DTOs.Amenity;
using Backend.Domain.Entities;

namespace Backend.Application.Interfaces
{
    public interface IAmenityService
    {
        Task<ICollection<AmenityResponseDTO>> GetAllAmenitiesAsync();
        Task<AmenityResponseDTO> CreateAmenityAsync(AmenityRequestDTO amenityRequestDTO);
        Task UpdateAmenityAsync(int amenityId, AmenityRequestDTO amenityRequestDTO);
        Task DeleteAmenityAsync(int amenityId);
    }
}
