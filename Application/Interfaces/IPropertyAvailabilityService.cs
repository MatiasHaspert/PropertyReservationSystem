using PropertyReservation.Application.DTOs.PropertyAvailability;

namespace PropertyReservation.Application.Interfaces
{
    public interface IPropertyAvailabilityService
    {
        Task<PropertyAvailabilityResponseDTO> CreatePropertyAvailabilityAsync(PropertyAvailabilityRequestDTO availabilityDto);
        Task<IEnumerable<PropertyAvailabilityResponseDTO>> GetPropertyAvailabilitiesAsync(int propertyId);
        Task DeletePropertyAvailabilityAsync(int availabilityId);
        Task UpdatePropertyAvailabilityAsync(int availabilityId, PropertyAvailabilityRequestDTO availabilityDto);
    }
}
