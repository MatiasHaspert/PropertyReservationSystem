using AutoMapper;
using Backend.Application.DTOs.Reservation;
using Backend.Domain.Entities;

namespace Backend.Application.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationResponseDTO>();

            CreateMap<ReservationRequestDTO, Reservation>();
        }
    }
}
