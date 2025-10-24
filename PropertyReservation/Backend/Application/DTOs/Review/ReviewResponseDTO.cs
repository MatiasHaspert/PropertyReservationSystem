using Backend.Domain.Entities;

namespace Backend.Application.DTOs.Review
{
    public class ReviewResponseDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; } // Calificación del 1 al 5
        public string Comment { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int PropertyId { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }

    }
}
