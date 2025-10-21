namespace Backend.Application.DTOs.Reservation
{
    public class ReservationRequestDTO
    {
        public int PropertyId { get; set; }
        public int GuestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalGuests { get; set; }
    }
}
