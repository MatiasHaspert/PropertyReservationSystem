using Backend.Domain.Enums;

namespace Backend.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;
        public int GuestId { get; set; }
        public User Guest { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalGuests { get; set; }
        // Precio final calculado
        public decimal TotalPrice { get; set; }
        // Estado de la reserva
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
