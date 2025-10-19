using AutoMapper;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Application.DTOs.Review;

namespace PropertyReservation.Application.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewResponseDTO>();
            CreateMap<ReviewRequestDTO, Review>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
