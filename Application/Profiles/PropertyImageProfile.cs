using AutoMapper;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Application.DTOs.PropertyImage;

namespace PropertyReservation.Application.Profiles
{
    public class PropertyImageProfile : Profile
    {
        public PropertyImageProfile()
        {
            CreateMap<PropertyImage, PropertyImageResponseDTO>();
        }
    }
}
