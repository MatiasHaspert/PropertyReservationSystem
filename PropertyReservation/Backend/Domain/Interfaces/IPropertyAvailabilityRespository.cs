using Backend.Application.DTOs.PropertyAvailability;
using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IPropertyAvailabilityRespository
    {
        Task<IEnumerable<PropertyAvailability>> GetPropertyAvailabilitiesAsync(int propertyId);
        Task<PropertyAvailability> CreatePropertyAvailabilityAsync(PropertyAvailability availability);
        Task UpdatePropertyAvailabilityAsync(PropertyAvailability availability);
        Task DeletePropertyAvailabilityAsync(int availabilityId);
        Task<bool> HasOverlappingAvailabilityAsync(PropertyAvailabilityRequestDTO availabilityDTO, int ? excludeAvailabilityId);
        Task<bool> PropertyAvailabilityExistsAsync(int availabilityId);
        Task<PropertyAvailability?> GetByIdAsync(int availabilityId);
    }
}
