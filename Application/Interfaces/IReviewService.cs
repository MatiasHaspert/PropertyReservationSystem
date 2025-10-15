using PropertyReservation.Domain.Entities;
using PropertyReservation.Application.DTOs.Review;
namespace PropertyReservation.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseDTO>> GetPropertyReviewsAsync(int propertyId);
        Task<ReviewResponseDTO> GetPropertyReviewByIdAsync(int propertyId, int reviewId);

        Task<ReviewResponseDTO> CreateReviewAsync(ReviewRequestDTO reviewRequestDTO);
        Task<bool> UpdateReviewAsync(int reviewId, ReviewRequestDTO reviewRequestDTO);
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}
