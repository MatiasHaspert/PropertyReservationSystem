namespace PropertyReservation.Application.DTOs.PropertyImage
{
    public class PropertyImageResponseDTO
    {
        public int Id { get; set; }                
        public string Url { get; set; } = string.Empty;  
        public bool IsMainImage { get; set; }       
        public DateTime CreatedAt { get; set; }      
    }
}
