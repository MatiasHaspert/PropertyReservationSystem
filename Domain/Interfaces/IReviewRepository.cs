using PropertyReservation.Domain.Entities;
namespace PropertyReservation.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetPropertyReviewsAsync(int propertyId);
        //Task<Review?> GetByIdAsync(int id);
        //Task<Review> AddAsync(Review review);
        //Task UpdateAsync(Review review);
        //Task DeleteAsync(int id);
        //bool Exists(int id);


    }
}
