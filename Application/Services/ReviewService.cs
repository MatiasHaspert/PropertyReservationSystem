using PropertyReservation.Application.Interfaces;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.Interfaces;
using PropertyReservation.Application.DTOs.Review;

namespace PropertyReservation.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewResponseDTO> CreateReviewAsync(ReviewRequestDTO reviewRequestDTO)
        {
            var review = MapDTOToReview(reviewRequestDTO);
            return await _reviewRepository.CreateReviewAsync(review)
                .ContinueWith(t => MapReviewToDTO(t.Result));
        }

        public async Task<ReviewResponseDTO> GetPropertyReviewByIdAsync(int propertyId, int reviewId)
        {
            var review = await _reviewRepository.GetPropertyReviewByIdAsync(propertyId, reviewId);
            if (review == null)
            {
                throw new KeyNotFoundException("ID no encontrado.");
            }
            return MapReviewToDTO(review);
        }

        public async Task<IEnumerable<ReviewResponseDTO>> GetPropertyReviewsAsync(int propertyId)
        {
            var reviews = await _reviewRepository.GetPropertyReviewsAsync(propertyId);
            return reviews.Select(r => MapReviewToDTO(r)).ToList();
        }

        public async Task UpdateReviewAsync(int reviewId, ReviewRequestDTO reviewRequestDTO)
        {
            if(! await _reviewRepository.ReviewExistsAsync(reviewId))
            {
                throw new KeyNotFoundException("Reseña no encontrada.");
            }
            var review = MapDTOToReview(reviewRequestDTO);
            review.Id = reviewId; 
            await _reviewRepository.UpdateReviewAsync(review);
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            if(!await _reviewRepository.ReviewExistsAsync(reviewId))
            {
                throw new KeyNotFoundException("Reseña no encontrada.");
            }

            await _reviewRepository.DeleteAsync(reviewId);
        }

        private ReviewResponseDTO MapReviewToDTO(Review review)
        {
            return new ReviewResponseDTO
            {
                Id = review.Id,
                PropertyId = review.PropertyId,
                Rating = review.Rating,
                Comment = review.Comment,
                UserId = review.UserId,
                Date = review.Date
            };
        }

        private Review MapDTOToReview(ReviewRequestDTO reviewRequestDTO)
        {
            return new Review
            {
                PropertyId = reviewRequestDTO.PropertyId,
                Rating = reviewRequestDTO.Rating,
                Comment = reviewRequestDTO.Comment,
                UserId = reviewRequestDTO.UserId,
                Date = DateTime.UtcNow
            };
        }
    }
}
