namespace ReservaPropiedades.Application.DTOs.Propiedad
{
    public class PropiedadListResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public decimal PrecioPorNoche { get; set; }
        public int CapacidadHuespedes { get; set; }
        public int NumeroHabitaciones { get; set; }
        public int NumeroBaños { get; set; }
        public int calificacionPromedio { get; set; }
        public UbicacionDTO Ubicacion { get; set; } = new UbicacionDTO();
        // Imagen principal de la propiedad
        //public string ImagenPrincipal { get; set; } = string.Empty;
    }
}
