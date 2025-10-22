using AutoMapper;
using Backend.Application.DTOs.PropertyAvailability;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Repositories;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Services
{
    public class PropertyAvailabilityService : IPropertyAvailabilityService
    {
        private readonly IPropertyAvailabilityRespository _availabilityRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public PropertyAvailabilityService(
            IPropertyAvailabilityRespository availabilityRepository,
            IPropertyRepository propertyRepository,
            IReservationRepository reservationRepository,
            IMapper mapper
        )
        {
            _availabilityRepository = availabilityRepository;
            _propertyRepository = propertyRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyAvailabilityResponseDTO>> GetPropertyAvailabilitiesAsync(int propertyId)
        {
            if (!await _propertyRepository.PropertyExistsAsync(propertyId))
                throw new ArgumentException("La propiedad indicada no existe.");

            var availabilities = await _availabilityRepository.GetPropertyAvailabilitiesAsync(propertyId);
            return availabilities.Select(a => _mapper.Map<PropertyAvailabilityResponseDTO>(a)).ToList();
        }

        public async Task<PropertyAvailabilityResponseDTO> CreatePropertyAvailabilityAsync(PropertyAvailabilityRequestDTO availabilityDto)
        {
            ValidateAvailabilityDates(availabilityDto.StartDate, availabilityDto.EndDate);

            // Verificar existencia de la propiedad
            var propertyExists = await _propertyRepository.PropertyExistsAsync(availabilityDto.PropertyId);
            if (!propertyExists)
                throw new InvalidOperationException("No se puede crear la disponibilidad: la propiedad no existe.");

            // Verificar solapamiento de fechas
            var hasOverlap = await _availabilityRepository.HasOverlappingAvailabilityAsync(availabilityDto, null);
            if (hasOverlap)
                throw new InvalidOperationException("La disponibilidad se solapa con una existente.");

            var availability = _mapper.Map<PropertyAvailability>(availabilityDto);
            var createdAvailability = await _availabilityRepository.CreatePropertyAvailabilityAsync(availability);
            return _mapper.Map<PropertyAvailabilityResponseDTO>(createdAvailability);
        }

        public async Task UpdatePropertyAvailabilityAsync(int availabilityId, PropertyAvailabilityRequestDTO availabilityDto)
        {
            ValidateAvailabilityDates(availabilityDto.StartDate, availabilityDto.EndDate);

            // Verificar existencia de la disponibilidad
            var availabilityExists = await _availabilityRepository.PropertyAvailabilityExistsAsync(availabilityId);
            if (!availabilityExists)
                throw new ArgumentException("La disponibilidad indicada no existe.");

            // Verificar existencia de la propiedad
            var propertyExists = await _propertyRepository.PropertyExistsAsync(availabilityDto.PropertyId);
            if (!propertyExists)
                throw new InvalidOperationException("No se puede crear la disponibilidad: la propiedad no existe.");

            // Verificar solapamiento de fechas, excluyendo la disponibilidad que se está actualizando
            var hasOverlap = await _availabilityRepository.HasOverlappingAvailabilityAsync(availabilityDto, availabilityId);
            if (hasOverlap)
                throw new InvalidOperationException("La disponibilidad se solapa con una existente.");

            var availability = _mapper.Map<PropertyAvailability>(availabilityDto);
            availability.Id = availabilityId; // Asegurar que el ID se mantiene para la actualización
            await _availabilityRepository.UpdatePropertyAvailabilityAsync(availability);
        }

        public async Task DeletePropertyAvailabilityAsync(int availabilityId)
        {
            // Obtener la disponibilidad
            var availability = await _availabilityRepository.GetByIdAsync(availabilityId);
            if (availability == null)
                throw new ArgumentException("La disponibilidad indicada no existe.");

            // Buscar reservas superpuestas
            bool hasOverlappingReservations = await _reservationRepository.HasOverlappingReservationAsync(
                availability.PropertyId,
                availability.StartDate,
                availability.EndDate,
                null
            );

            if (hasOverlappingReservations)
                throw new InvalidOperationException("No se puede eliminar la disponibilidad porque existen reservas confirmadas o pendientes dentro del rango.");

            // Eliminar si no hay conflictos
            await _availabilityRepository.DeletePropertyAvailabilityAsync(availabilityId);
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

    }
}
