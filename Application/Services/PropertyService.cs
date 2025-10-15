using PropertyReservation.Application.Interfaces;
using PropertyReservation.Domain.Interfaces;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.ValueObjects;
using PropertyReservation.Application.DTOs.Property;

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

        public async Task<bool> PutPropertyAsync(int id, PropertyRequestDTO propertyDTO)
        {
            if (!PropertyExists(id))
            {
                return false;
            }
            Property property = mapearDtoProperty(propertyDTO);
            await _propertyRepository.UpdateAsync(property);
            return true;
        }

        public async Task<Property> CreatePropertyAsync(PropertyRequestDTO propertyDTO)
        {
            Property property = mapearDtoProperty(propertyDTO);
            return await _propertyRepository.AddAsync(property);
        }

        public async Task<bool> DeletePropertyAsync(int id)
        {
            if (!PropertyExists(id))
            {
                return false;
            }
            await _propertyRepository.DeleteAsync(id);
            return true;
        }

        public bool PropertyExists(int id)
        {
            return _propertyRepository.Exists(id);
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
