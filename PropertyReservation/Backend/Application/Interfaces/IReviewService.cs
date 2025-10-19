using Backend.Domain.Entities;
using Backend.Application.DTOs.Review;
namespace Backend.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseDTO>> GetPropertyReviewsAsync(int propertyId);
        Task<ReviewResponseDTO> GetPropertyReviewByIdAsync(int propertyId, int reviewId);
        Task<ReviewResponseDTO> CreateReviewAsync(ReviewRequestDTO reviewRequestDTO);
        Task UpdateReviewAsync(int reviewId, ReviewRequestDTO reviewRequestDTO);
        Task DeleteReviewAsync(int reviewId);
    }
}
