using AutoMapper;
using Azure.Core;
using Backend.Application.DTOs.Property;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Backend.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyAvailabilityRespository _availabilityRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public PropertyService(
            IPropertyRepository propertyRepository, 
            IUserRepository userRepository, 
            IAmenityRepository amenityRepository, 
            IPropertyImageRepository propertyImageRepository, 
            IMapper mapper,
            IPropertyAvailabilityRespository availabilityRepository,
            IReservationRepository reservationRepository
        )
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _amenityRepository = amenityRepository;
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
            _availabilityRepository = availabilityRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<PropertyListResponseDTO> GetPropertyByIdAsync(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            return _mapper.Map<PropertyListResponseDTO>(property);
        }
        public async Task<IEnumerable<PropertyListResponseDTO>> GetPropertyListAsync()
        {
            var properties =  await _propertyRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<PropertyListResponseDTO>>(properties);
        }


        public async Task<PropertyDetailsResponseDTO> GetPropertyDetailsByIdAsync(int id)
        {
            var property = await _propertyRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<PropertyDetailsResponseDTO>(property);
        }

        public async Task<PropertyListResponseDTO> CreatePropertyAsync(PropertyRequestDTO propertyDTO)
        {
            Property property = _mapper.Map<Property>(propertyDTO);

            var owner = await _userRepository.GetByIdAsync(propertyDTO.OwnerId);
            if (owner is null)
            {
                throw new ArgumentException($"El usuario con ID {propertyDTO.OwnerId} no existe.");
            }
            property.Owner = owner;

            if (propertyDTO.AmenityIds.Any())
            {
                property.Amenities = await _amenityRepository.GetAmenitiesByIdsAsync(propertyDTO.AmenityIds);
            }

            var propertyCreate = await _propertyRepository.AddAsync(property);
            return _mapper.Map<PropertyListResponseDTO>(propertyCreate);
        }

        public async Task<PropertyCalendarDTO> GetPropertyCalendarAsync(int propertyId)
        {
            var property = await _propertyRepository.GetByIdAsync(propertyId);
            if (property == null)
                throw new ArgumentException("Propiedad no encontrada.");

            var availabilities = await _availabilityRepository.GetPropertyAvailabilitiesAsync(propertyId);
            var reservations = await _reservationRepository.GetReservationsByPropertyIdAsync(propertyId);

            return new PropertyCalendarDTO
            {
                PropertyId = propertyId,
                AvailableRanges = availabilities.Select(a => new CalendarRangeDTO
                {
                    StartDate = a.StartDate,
                    EndDate = a.EndDate
                }).ToList(),
                ReservedRanges = reservations
                    .Where(r => r.Status == ReservationStatus.Confirmed || r.Status == ReservationStatus.Pending)
                    .Select(r => new CalendarRangeDTO
                    {
                        StartDate = r.StartDate,
                        EndDate = r.EndDate
                    }).ToList()
            };
        }

        public async Task PutPropertyAsync(int id, PropertyRequestDTO propertyDTO)
        {
            if (!await PropertyExistsAsync(id))
            {
                throw new KeyNotFoundException("Propiedad no encontrada.");
            }
            Property property = _mapper.Map<Property>(propertyDTO);

            if (propertyDTO.AmenityIds.Any())
            {
                property.Amenities = await _amenityRepository.GetAmenitiesByIdsAsync(propertyDTO.AmenityIds);
            }

            await _propertyRepository.UpdateAsync(property);
        }

        public async Task DeletePropertyAsync(int id)
        {
            if (!await PropertyExistsAsync(id))
            {
                throw new KeyNotFoundException("Propiedad no encontrada.");
            }
            await _propertyRepository.DeleteAsync(id);
        }

        public async Task<bool> PropertyExistsAsync(int id)
        {
            return await _propertyRepository.PropertyExistsAsync(id);
        }
    }
}
