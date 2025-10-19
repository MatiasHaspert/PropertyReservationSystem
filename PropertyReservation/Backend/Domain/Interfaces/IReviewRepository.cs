using Backend.Domain.Entities;
namespace Backend.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetPropertyReviewsAsync(int propertyId);
        Task<Review?> GetPropertyReviewByIdAsync(int propertyId, int reviewId);
        Task<Review> CreateReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteAsync(int reviewId);
        Task<bool> ReviewExistsAsync(int reviewId);
    }
}
