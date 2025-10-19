using AutoMapper;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Application.DTOs.Review;

namespace PropertyReservation.Application.Profiles
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
