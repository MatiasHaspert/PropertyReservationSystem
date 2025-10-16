namespace PropertyReservation.Application.DTOs.PropertyAvailability
{
    public class PropertyAvailabilityResponseDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PropertyId { get; set; }
    }
}
