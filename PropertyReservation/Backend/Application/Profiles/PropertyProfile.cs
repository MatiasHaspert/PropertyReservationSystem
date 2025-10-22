using AutoMapper;
using Backend.Application.DTOs.Amenity;
using Backend.Application.DTOs.Property;
using Backend.Application.DTOs.PropertyImage;
using Backend.Application.DTOs.Review;
using Backend.Domain.Entities;

namespace Backend.Application.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            // Mapeo para la lista de propiedades (resumen)
            CreateMap<Property, PropertyListResponseDTO>()
                // Mapea la imagen principal (si existe)
                .ForMember(dest => dest.MainImage,
                           opt => opt.MapFrom(src =>
                               src.Images.FirstOrDefault(i => i.IsMainImage)))

                // Calcula el promedio de reseñas (si hay reseñas)
                .ForMember(dest => dest.AverageRating,
                           opt => opt.MapFrom(src =>
                               src.Reviews.Any()
                                   ? src.Reviews.Average(r => r.Rating)
                                   : 0));

            // Mapeo detallado (para ver una propiedad completa)
            CreateMap<Property, PropertyDetailsResponseDTO>()
                .ForMember(dest => dest.AverageRating,
                    opt => opt.MapFrom(src =>
                        src.Reviews.Any()
                            ? src.Reviews.Average(r => r.Rating)
                            : 0))
                .ForMember(dest => dest.Amenities,
                    opt => opt.MapFrom(src => src.Amenities))
                .ForMember(dest => dest.Images,
                    opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Reviews,
                    opt => opt.MapFrom(src => src.Reviews));

            CreateMap<PropertyRequestDTO, Property>();

        }
    }
}
