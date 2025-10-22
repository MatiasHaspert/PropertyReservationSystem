using Backend.Domain.Enums;

namespace Backend.Application.DTOs.Reservation
{
    public class UpdateReservationStatusDTO
    {
        public ReservationStatus Status { get; set; }
    }
}
