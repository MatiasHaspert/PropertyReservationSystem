using AutoMapper;
using Backend.Domain.Entities;
using Backend.Application.DTOs.PropertyImage;

namespace Backend.Application.Profiles
{
    public class PropertyImageProfile : Profile
    {
        public PropertyImageProfile()
        {
            CreateMap<PropertyImage, PropertyImageResponseDTO>();

        }
    }
}
