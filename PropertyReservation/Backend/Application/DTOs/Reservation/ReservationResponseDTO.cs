namespace Backend.Application.DTOs.Reservation
{
    public class ReservationResponseDTO
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        //public string PropertyTitle { get; set; } = string.Empty;
        public int GuestId { get; set; }
        //public string GuestName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
