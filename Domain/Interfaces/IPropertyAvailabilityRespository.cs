using PropertyReservation.Application.DTOs.PropertyAvailability;
using PropertyReservation.Domain.Entities;

namespace PropertyReservation.Domain.Interfaces
{
    public interface IPropertyAvailabilityRespository
    {
        Task<PropertyAvailability> CreatePropertyAvailabilityAsync(PropertyAvailability availability);
        Task<IEnumerable<PropertyAvailability>> GetPropertyAvailabilitiesAsync(int propertyId);
        Task DeletePropertyAvailabilityAsync(int availabilityId);
        Task UpdatePropertyAvailabilityAsync(PropertyAvailability availability);
        Task<bool> HasOverlappingAvailabilityAsyncCreate(PropertyAvailabilityRequestDTO availabilityDTO);
        Task<bool> HasOverlappingAvailabilityAsyncUpdate(int availabilityId, PropertyAvailabilityRequestDTO availabilityDTO);
        Task<bool> PropertyAvailabilityExistsAsync(int availabilityId);
    }
}
