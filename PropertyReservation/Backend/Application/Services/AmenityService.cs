using AutoMapper;
using Backend.Application.DTOs.Amenity;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public AmenityService(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<AmenityResponseDTO>> GetAllAmenitiesAsync()
        {
            var amenities = await _amenityRepository.GetAllAmenitiesAsync();
            return amenities.Select(a => _mapper.Map<AmenityResponseDTO>(a)).ToList();
        }
        
        public async Task<AmenityResponseDTO> CreateAmenityAsync(AmenityRequestDTO amenityRequestDTO)
        {
            var amenity = _mapper.Map<Amenity>(amenityRequestDTO);
            return await _amenityRepository.CreateAmenityAsync(amenity).ContinueWith(a => _mapper.Map<AmenityResponseDTO>(a));
        }
        
        public async Task UpdateAmenityAsync(int amenityId, AmenityRequestDTO amenityRequestDTO)
        {
            var amenityExists = await _amenityRepository.AmenityExistsAsync(amenityId);
            if (!amenityExists)
            {
                throw new ArgumentException("Servicio no encontrado.");
            }
            var amenity = _mapper.Map<Amenity>(amenityRequestDTO);
            amenity.Id = amenityId;
            await _amenityRepository.UpdateAmenityAsync(amenity);
        }

        public async Task DeleteAmenityAsync(int amenityId)
        {
            var amenityExists = await _amenityRepository.AmenityExistsAsync(amenityId);
            if (!amenityExists)
            {
                throw new ArgumentException("Servicio no encontrado.");
            }
            await _amenityRepository.DeleteAsync(amenityId);
        }

    }
}
