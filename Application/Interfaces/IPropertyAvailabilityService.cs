using PropertyReservation.Application.DTOs.PropertyAvailability;

namespace PropertyReservation.Application.Interfaces
{
    public interface IPropertyAvailabilityService
    {
        Task<IEnumerable<PropertyAvailabilityResponseDTO>> GetPropertyAvailabilitiesAsync(int propertyId);
        Task<PropertyAvailabilityResponseDTO> CreatePropertyAvailabilityAsync(PropertyAvailabilityRequestDTO availabilityDto);
        Task DeletePropertyAvailabilityAsync(int availabilityId);
        Task UpdatePropertyAvailabilityAsync(int availabilityId, PropertyAvailabilityRequestDTO availabilityDto);
    }
}
