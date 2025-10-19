using AutoMapper;
using Backend.Domain.ValueObjects;
using Backend.Application.DTOs;

namespace Backend.Application.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
