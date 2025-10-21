using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<bool> HasOverlappingReservationAsync(int propertyId, DateTime startDate, DateTime endDate)
        {
            return await _context.Reservations
                .AnyAsync(r =>
                    r.PropertyId == propertyId &&
                    (r.Status == ReservationStatus.Confirmed || r.Status == ReservationStatus.Pending) &&
                    r.StartDate <= endDate && r.EndDate >= startDate
                );
        }

    }
}
