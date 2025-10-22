
using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> AddReservationAsync(Reservation reservation);
        Task<bool> HasOverlappingReservationAsync(int propertyId, DateTime startDate, DateTime endDate, int? excludeReservationId);
        Task<Reservation?> GetByIdAsync(int reservationId);
        Task UpdateAsync (Reservation reservation);
        Task<IEnumerable<Reservation>> GetReservationsByPropertyIdAsync(int propertyId);

    }
}
