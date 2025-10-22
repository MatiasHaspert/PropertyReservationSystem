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

        public async Task<bool> HasOverlappingReservationAsync(
            int propertyId,
            DateTime startDate,
            DateTime endDate,
            int? excludeReservationId = null)
        {
            return await _context.Reservations
                .AnyAsync(r =>
                    r.PropertyId == propertyId &&
                    r.Status != ReservationStatus.Cancelled && // solo reservas activas
                    (excludeReservationId == null || r.Id != excludeReservationId) && // excluir actual
                    startDate <= r.EndDate &&
                    endDate >= r.StartDate
                );
        }


        public async Task<Reservation?> GetByIdAsync(int reservationId)
        {
            return await _context.Reservations.FindAsync(reservationId);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByPropertyIdAsync(int propertyId)
        {
            return await _context.Reservations
                .Where(r => r.PropertyId == propertyId)
                .ToListAsync();
        }

    }
}
