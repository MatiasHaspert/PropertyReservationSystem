namespace ReservaPropiedades.Domain.ValueObjects
{
    public class Ubicacion
    {
        public string Ciudad { get; set; } 
        public string Pais { get; set; }
        public string Direccion { get; set; }
        public int CodigoPostal { get; set; }
        public string Provincia { get; set; }

        public Ubicacion() 
        {
            Ciudad = string.Empty;
            Pais = string.Empty;
            Direccion = string.Empty;
            CodigoPostal = 0;
            Provincia = string.Empty;
        }
        public Ubicacion(string ciudad, string pais, string direccion, int codigoPostal, string provincia)
        {
            Ciudad = ciudad;
            Pais = pais;
            Direccion = direccion;
            CodigoPostal = codigoPostal;
            Provincia = provincia;
        }

    }
}
