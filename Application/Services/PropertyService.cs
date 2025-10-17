using PropertyReservation.Application.Interfaces;
using PropertyReservation.Domain.Interfaces;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.ValueObjects;
using PropertyReservation.Application.DTOs.Property;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PropertyReservation.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            return await _propertyRepository.GetAllAsync();
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            return await _propertyRepository.GetByIdAsync(id);
        }

        public async Task PutPropertyAsync(int id, PropertyRequestDTO propertyDTO)
        {
            if (!await PropertyExistsAsync(id))
            {
                throw new KeyNotFoundException("Propiedad no encontrada.");
            }
            Property property = mapearDtoProperty(propertyDTO);
            await _propertyRepository.UpdateAsync(property);
        }

        public async Task<Property> CreatePropertyAsync(PropertyRequestDTO propertyDTO)
        {
            Property property = mapearDtoProperty(propertyDTO);
            return await _propertyRepository.AddAsync(property);
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

        // Posibilidad de luego usar AutoMapper
        public Property mapearDtoProperty(PropertyRequestDTO dto)
        {
            return new Property
            {
                Title = dto.Title,
                Description = dto.Description,
                NightlyPrice = dto.NightlyPrice,
                MaxGuests = dto.MaxGuests,
                Address = new Address
                {
                    City = dto.Address.City,
                    Country = dto.Address.Country,
                    StreetAddress = dto.Address.StreetAddress,
                    PostalCode = dto.Address.PostalCode
                }
            };
        }
    }
}
