using Backend.Domain.Entities;
using Backend.Application.DTOs.Property;
namespace Backend.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyListResponseDTO>> GetPropertyListAsync();
        Task<PropertyDetailsResponseDTO> GetPropertyDetailsByIdAsync(int id);
        Task<PropertyListResponseDTO> CreatePropertyAsync(PropertyRequestDTO propertyDTO);
        Task PutPropertyAsync(int id, PropertyRequestDTO propertyDTO);
        Task DeletePropertyAsync(int id);
        Task<bool> PropertyExistsAsync(int id);
        Task<PropertyListResponseDTO> GetPropertyByIdAsync(int id);
        Task<PropertyCalendarDTO> GetPropertyCalendarAsync(int property_id);
    }
}
