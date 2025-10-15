namespace PropertyReservation.Application.DTOs.Review
{
    public class ReviewRequestDTO
    {
        public int Rating { get; set; } // Calificación del 1 al 5
        public string Comment { get; set; } = string.Empty;
        public int PropertyId { get; set; }
        public int UserId { get; set; }
    }
}
