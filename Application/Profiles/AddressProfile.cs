using AutoMapper;
using PropertyReservation.Domain.ValueObjects;
using PropertyReservation.Application.DTOs;

namespace PropertyReservation.Application.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
