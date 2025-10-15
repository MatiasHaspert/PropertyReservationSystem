using PropertyReservation.Application.DTOs.Amenity;
using PropertyReservation.Application.Interfaces;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.Interfaces;

namespace PropertyReservation.Application.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IAmenityRepository _amenityRepository;

        public AmenityService(IAmenityRepository amenityRepository)
        {
            _amenityRepository = amenityRepository;
        }

        public async Task<AmenityResponseDTO> CreateAmenityAsync(AmenityRequestDTO amenityRequestDTO)
        {
            var amenity = MapDtoToAmenity(amenityRequestDTO);
            return await _amenityRepository.CreateAmenityAsync(amenity).ContinueWith(a => MapAmenityToDto(a.Result));
        }

        public async Task<bool> DeleteAmenityAsync(int amenityId)
        {
            if(!_amenityRepository.AmenityExists(amenityId))
            {
                return false;
            }
            await _amenityRepository.DeleteAsync(amenityId);
            return true;
        }

        public async Task<IEnumerable<AmenityResponseDTO>> GetAllAmenitiesAsync()
        {
            var amenities = await _amenityRepository.GetAllAmenitiesAsync();
            return amenities.Select(a => MapAmenityToDto(a)).ToList();
        }

        public async Task<bool> UpdateAmenityAsync(int amenityId, AmenityRequestDTO amenityRequestDTO)
        {
            if (!_amenityRepository.AmenityExists(amenityId))
            {
                return false;
            }
            var amenity = MapDtoToAmenity(amenityRequestDTO);
            amenity.Id = amenityId;
            await _amenityRepository.UpdateAmenityAsync(amenity);
            return true;
        }

        private AmenityResponseDTO MapAmenityToDto(Amenity amenity)
        {
            return new AmenityResponseDTO
            {
                Id = amenity.Id,
                Name = amenity.Name,
            };
        }

        private Amenity MapDtoToAmenity(AmenityRequestDTO dto)
        {
            return new Amenity
            {
                Name = dto.Name,
            };
        }
    }
}
