using AutoMapper;
using Backend.Domain.Entities;
using Backend.Application.DTOs.PropertyAvailability;

namespace Backend.Application.Profiles
{
    public class PropertyAvailabilityProfile : Profile
    {
        public PropertyAvailabilityProfile()
        {
            CreateMap<PropertyAvailability, PropertyAvailabilityResponseDTO>();
            CreateMap<PropertyAvailabilityRequestDTO, PropertyAvailability>();
        }
    }
}
