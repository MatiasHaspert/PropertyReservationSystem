using Humanizer;
using Microsoft.EntityFrameworkCore;
using PropertyReservation.Application.DTOs.PropertyAvailability;
using PropertyReservation.Application.Interfaces;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.Interfaces;

namespace PropertyReservation.Application.Services
{
    public class PropertyAvailabilityService : IPropertyAvailabilityService
    {
        private readonly IPropertyAvailabilityRespository _availabilityRepository;
        private readonly IPropertyRepository _propertyRepository;
        public PropertyAvailabilityService(IPropertyAvailabilityRespository availabilityRepository,
                                           IPropertyRepository propertyRepository)
        {
            _availabilityRepository = availabilityRepository;
            _propertyRepository = propertyRepository;
        }

        public async Task<PropertyAvailabilityResponseDTO> CreatePropertyAvailabilityAsync(PropertyAvailabilityRequestDTO availabilityDto)
        {
            ValidateAvailabilityDates(availabilityDto.StartDate, availabilityDto.EndDate);

            // Verificar existencia de la propiedad
            var propertyExists = await _propertyRepository.PropertyExistsAsync(availabilityDto.PropertyId);
            if (!propertyExists)
                throw new InvalidOperationException("No se puede crear la disponibilidad: la propiedad no existe.");


            // Verificar solapamiento de fechas
            var hasOverlap = await _availabilityRepository.HasOverlappingAvailabilityAsyncCreate(availabilityDto);
            if (hasOverlap)
                throw new InvalidOperationException("La disponibilidad se solapa con una existente.");

            var availability = MapDTOToPropertyAvailability(availabilityDto);
            var createdAvailability = await _availabilityRepository.CreatePropertyAvailabilityAsync(availability);
            return MapPropertyAvailabilityToDTO(createdAvailability);
        }

        public async Task DeletePropertyAvailabilityAsync(int availabilityId)
        {
            if(! await _availabilityRepository.PropertyAvailabilityExistsAsync(availabilityId))
                throw new ArgumentException("La disponibilidad indicada no existe.");

            await _availabilityRepository.DeletePropertyAvailabilityAsync(availabilityId);
        }

        public async Task<IEnumerable<PropertyAvailabilityResponseDTO>> GetPropertyAvailabilitiesAsync(int propertyId)
        {
            var availabilities = await _availabilityRepository.GetPropertyAvailabilitiesAsync(propertyId);
            return availabilities.Select(a => MapPropertyAvailabilityToDTO(a)).ToList();
        }

        public async Task UpdatePropertyAvailabilityAsync(int availabilityId, PropertyAvailabilityRequestDTO availabilityDto)
        {
            ValidateAvailabilityDates(availabilityDto.StartDate, availabilityDto.EndDate);

            // Verificar existencia de la propiedad
            var propertyExists = await _propertyRepository.PropertyExistsAsync(availabilityDto.PropertyId);
            if (!propertyExists)
                throw new InvalidOperationException("No se puede crear la disponibilidad: la propiedad no existe.");

            // Verificar solapamiento de fechas, excluyendo la disponibilidad que se está actualizando
            var hasOverlap = await _availabilityRepository.HasOverlappingAvailabilityAsyncUpdate(availabilityId, availabilityDto);
            if (hasOverlap)
                throw new InvalidOperationException("La disponibilidad se solapa con una existente.");

            var availability = MapDTOToPropertyAvailability(availabilityDto);
            availability.Id = availabilityId; // Asegurar que el ID se mantiene para la actualización
            await _availabilityRepository.UpdatePropertyAvailabilityAsync(availability);
        }

        private void ValidateAvailabilityDates(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio.");

            double duration = (end - start).TotalDays;
            if (duration > 365)
                throw new ArgumentException("La disponibilidad no puede superar un año.");
            if (duration < 1)
                throw new ArgumentException("La disponibilidad debe durar al menos un día.");
        }

        private PropertyAvailabilityResponseDTO MapPropertyAvailabilityToDTO(PropertyAvailability availability)
        {
            return new PropertyAvailabilityResponseDTO
            {
                Id = availability.Id,
                PropertyId = availability.PropertyId,
                StartDate = availability.StartDate,
                EndDate = availability.EndDate,
            };
        }
        private PropertyAvailability MapDTOToPropertyAvailability(PropertyAvailabilityRequestDTO dto)
        {
            return new PropertyAvailability
            {
                PropertyId = dto.PropertyId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
            };
        }
    }
}
