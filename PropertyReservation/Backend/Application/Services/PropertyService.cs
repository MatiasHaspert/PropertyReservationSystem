using AutoMapper;
using Azure.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Backend.Application.DTOs.Property;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        public PropertyService(
            IPropertyRepository propertyRepository, 
            IUserRepository userRepository, 
            IAmenityRepository amenityRepository, 
            IPropertyImageRepository propertyImageRepository, 
            IMapper mapper
        )
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _amenityRepository = amenityRepository;
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public async Task<PropertyListResponseDTO> GetPropertyByIdAsync(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            return _mapper.Map<PropertyListResponseDTO>(property);
        }
        public async Task<IEnumerable<PropertyListResponseDTO>> GetPropertyListAsync()
        {
            var properties =  await _propertyRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<PropertyListResponseDTO>>(properties);
        }


        public async Task<PropertyDetailsResponseDTO> GetPropertyDetailsByIdAsync(int id)
        {
            var property = await _propertyRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<PropertyDetailsResponseDTO>(property);
        }

        public async Task<PropertyListResponseDTO> CreatePropertyAsync(PropertyRequestDTO propertyDTO)
        {
            Property property = _mapper.Map<Property>(propertyDTO);

            var owner = await _userRepository.GetByIdAsync(propertyDTO.OwnerId);
            if (owner is null)
            {
                throw new ArgumentException($"El usuario con ID {propertyDTO.OwnerId} no existe.");
            }
            property.Owner = owner;

            if (propertyDTO.AmenityIds.Any())
            {
                property.Amenities = await _amenityRepository.GetAmenitiesByIdsAsync(propertyDTO.AmenityIds);
            }

            var propertyCreate = await _propertyRepository.AddAsync(property);
            return _mapper.Map<PropertyListResponseDTO>(propertyCreate);
        }

        public async Task PutPropertyAsync(int id, PropertyRequestDTO propertyDTO)
        {
            if (!await PropertyExistsAsync(id))
            {
                throw new KeyNotFoundException("Propiedad no encontrada.");
            }
            Property property = _mapper.Map<Property>(propertyDTO);

            if (propertyDTO.AmenityIds.Any())
            {
                property.Amenities = await _amenityRepository.GetAmenitiesByIdsAsync(propertyDTO.AmenityIds);
            }

            await _propertyRepository.UpdateAsync(property);
        }

        public async Task DeletePropertyAsync(int id)
        {
            if (!await PropertyExistsAsync(id))
            {
                throw new KeyNotFoundException("Propiedad no encontrada.");
            }
            await _propertyRepository.DeleteAsync(id);
        }

        public async Task<bool> PropertyExistsAsync(int id)
        {
            return await _propertyRepository.PropertyExistsAsync(id);
        }
    }
}
