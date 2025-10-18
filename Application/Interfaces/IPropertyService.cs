using PropertyReservation.Domain.Entities;
using PropertyReservation.Application.DTOs.Property;
namespace PropertyReservation.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task<IEnumerable<PropertyListResponseDTO>> GetPropertyListAsync();
        Task<Property?> GetPropertyByIdAsync(int id);
        Task<PropertyDetailsResponseDTO> GetPropertyDetailsByIdAsync(int id);
        Task<Property> CreatePropertyAsync(PropertyRequestDTO propertyDTO);
        Task PutPropertyAsync(int id, PropertyRequestDTO propertyDTO);
        Task DeletePropertyAsync(int id);
        Task<bool> PropertyExistsAsync(int id);
    }
}
