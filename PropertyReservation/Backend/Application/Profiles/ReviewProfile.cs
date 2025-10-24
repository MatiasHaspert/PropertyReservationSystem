using AutoMapper;
using Backend.Domain.Entities;
using Backend.Application.DTOs.Review;

namespace Backend.Application.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewResponseDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<ReviewRequestDTO, Review>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
