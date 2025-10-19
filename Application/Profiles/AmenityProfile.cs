using AutoMapper;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Application.DTOs.Amenity;

namespace PropertyReservation.Application.Profiles
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
