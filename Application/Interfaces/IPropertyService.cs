using PropertyReservation.Domain.Entities;
using PropertyReservation.Application.DTOs.Property;
namespace PropertyReservation.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task<Property> GetPropertyByIdAsync(int id);
        Task<bool> PutPropertyAsync(int id, PropertyRequestDTO propertyDTO);
        Task<Property> CreatePropertyAsync(PropertyRequestDTO propertyDTO);
        Task<bool> DeletePropertyAsync(int id);
        bool PropertyExists(int id);
    }
}
