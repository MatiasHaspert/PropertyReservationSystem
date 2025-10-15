using PropertyReservation.Domain.Entities;
namespace PropertyReservation.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetPropertyReviewsAsync(int propertyId);

    }
}
