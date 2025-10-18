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
                /*
                .ForMember(dest => dest.User, opt => opt.Ignore()) // TODO ver
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // TODO ver
                .ForMember(dest => dest.Amenities, opt => opt.Ignore()) // TODO ver
                .ForMember(dest => dest.Images, opt => opt.Ignore()) // Se asignan en el servicio
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.Availabilities, opt => opt.Ignore());
                */
        }
    }
}
