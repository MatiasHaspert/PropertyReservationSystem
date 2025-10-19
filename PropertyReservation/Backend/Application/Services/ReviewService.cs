using AutoMapper;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Application.DTOs.Review;

namespace Backend.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public ReviewService(
            IReviewRepository reviewRepository,
            IPropertyRepository propertyRepository,
            IMapper mapper
        )
        {
            _reviewRepository = reviewRepository;
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<ReviewResponseDTO> GetPropertyReviewByIdAsync(int propertyId, int reviewId)
        {
            if(! await _propertyRepository.PropertyExistsAsync(propertyId))
            {
                throw new KeyNotFoundException("Propiedad no encontrada.");
            }

            var review = await _reviewRepository.GetPropertyReviewByIdAsync(propertyId, reviewId);
            if (review == null)
            {
                throw new KeyNotFoundException("Reseña no encontrado.");
            }
            return _mapper.Map<ReviewResponseDTO>(review); 
        }

        public async Task<IEnumerable<ReviewResponseDTO>> GetPropertyReviewsAsync(int propertyId)
        {
            var propertyExists = await _propertyRepository.PropertyExistsAsync(propertyId);
            if (!propertyExists)
            {
                throw new KeyNotFoundException("Propiedad no encontrada.");
            }
            var reviews = await _reviewRepository.GetPropertyReviewsAsync(propertyId);
            return reviews.Select(r => _mapper.Map<ReviewResponseDTO>(r)).ToList();
        }

        public async Task<ReviewResponseDTO> CreateReviewAsync(ReviewRequestDTO reviewRequestDTO)
        {
            var propertyExists = await _propertyRepository.PropertyExistsAsync(reviewRequestDTO.PropertyId);
            if (!propertyExists)
            {
                throw new KeyNotFoundException("Propiedad no encontrada.");
            }

            var review = _mapper.Map<Review>(reviewRequestDTO);
            return await _reviewRepository.CreateReviewAsync(review)
                .ContinueWith(t => _mapper.Map<ReviewResponseDTO>(t.Result));
        }

        public async Task UpdateReviewAsync(int reviewId, ReviewRequestDTO reviewRequestDTO)
        {
            var propertyExists =  await _propertyRepository.PropertyExistsAsync(reviewRequestDTO.PropertyId);
            if (!propertyExists)
            {
                throw new KeyNotFoundException("Propiedad no encontrada.");
            }

            var reviewExists =  await _reviewRepository.ReviewExistsAsync(reviewId);
            if (!reviewExists)
            {
                throw new KeyNotFoundException("Reseña no encontrada.");
            }
            var review = _mapper.Map<Review>(reviewRequestDTO);
            review.Id = reviewId; 
            await _reviewRepository.UpdateReviewAsync(review);
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            var reviewExists = await _reviewRepository.ReviewExistsAsync(reviewId);
            if (!reviewExists)
            {
                throw new KeyNotFoundException("Reseña no encontrada.");
            }

            await _reviewRepository.DeleteAsync(reviewId);
        }

    }
}
