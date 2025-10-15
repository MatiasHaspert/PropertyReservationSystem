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
            return review == null ? null : MapReviewToDTO(review);
        }

        public async Task<IEnumerable<ReviewResponseDTO>> GetPropertyReviewsAsync(int propertyId)
        {
            var reviews = await _reviewRepository.GetPropertyReviewsAsync(propertyId);

            return reviews.Select(r => MapReviewToDTO(r)).ToList();
        }


        public async Task<bool> UpdateReviewAsync(int reviewId, ReviewRequestDTO reviewRequestDTO)
        {
            if(!_reviewRepository.ReviewExists(reviewId))
            {
                return false;
            }
            var review = MapDTOToReview(reviewRequestDTO);
            review.Id = reviewId; // Ensure the ID is set for the update
            await _reviewRepository.UpdateReviewAsync(review);
            return true;
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            if(!_reviewRepository.ReviewExists(reviewId))
            {
                return false;
            }

            await _reviewRepository.DeleteAsync(reviewId);
            return true;
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
