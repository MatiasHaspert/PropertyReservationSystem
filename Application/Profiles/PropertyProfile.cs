using AutoMapper;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Application.DTOs.Property;

namespace PropertyReservation.Application.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyListResponseDTO>()
                .ForMember(dest => dest.MainImage,
                           opt => opt.MapFrom(src => src.Images.FirstOrDefault()))
                .ForMember(dest => dest.AverageRating,
                           opt => opt.MapFrom(src => src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0));
            CreateMap<Property, PropertyDetailsResponseDTO>()
                .ForMember(dest => dest.AverageRating,
                           opt => opt.MapFrom(src => src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0));
            CreateMap<PropertyRequestDTO, Property>();
        }
    }
}
