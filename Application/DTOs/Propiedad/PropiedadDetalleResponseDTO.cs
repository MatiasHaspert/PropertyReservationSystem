namespace ReservaPropiedades.Application.DTOs.Propiedad
{
    public class PropiedadDetalleResponseDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioPorNoche { get; set; }
        public int CapacidadHuespedes { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public UbicacionDTO Ubicacion { get; set; } = new UbicacionDTO();
        // Luego UsuarioDTO, anfitrion
        //public UsuarioDTO Anfitrion { get; set; } = new UsuarioDTO();
        // Luego van los dtos de servicios, imagenes y reseñas
        public List<string> Servicios { get; set; } = new List<string>();
        public List<string> Imagenes { get; set; } = new List<string>();
        
        public List<string> Reseñas { get; set; } = new List<string>();
    }
}
