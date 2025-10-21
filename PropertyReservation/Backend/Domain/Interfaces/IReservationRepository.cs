
using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> AddReservationAsync(Reservation reservation);
        Task<bool> HasOverlappingReservationAsync(int propertyId, DateTime startDate, DateTime endDate);
    }
}
