namespace PropertyReservation.Application.DTOs.Property
{
    public class PropiedadListResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal NightlyPrice { get; set; }
        public int MaxGuests { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int calificacionPromedio { get; set; }
        public AddressDTO Address { get; set; } = new AddressDTO();
        // Imagen principal de la property
        //public string ImagenPrincipal { get; set; } = string.Empty;
    }
}
