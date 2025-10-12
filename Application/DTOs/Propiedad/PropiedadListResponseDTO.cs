namespace ReservaPropiedades.Application.DTOs.Propiedad
{
    public class PropiedadListResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public decimal PrecioPorNoche { get; set; }
        public int CapacidadHuespedes { get; set; }
        public UbicacionDTO Ubicacion { get; set; } = new UbicacionDTO();
        // Imagen principal de la propiedad
        //public string ImagenPrincipal { get; set; } = string.Empty;

    }
}
