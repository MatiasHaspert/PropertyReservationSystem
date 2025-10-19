using AutoMapper;
using Backend.Domain.Entities;
using Backend.Application.DTOs.Amenity;

namespace Backend.Application.Profiles
{
    public class AmenityProfile : Profile
    {
        public AmenityProfile()
        {
            CreateMap<Amenity, AmenityResponseDTO>();
            CreateMap<AmenityRequestDTO, Amenity>();
        }
    }
}
