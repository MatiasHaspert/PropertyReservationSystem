using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyReservation.Application.DTOs.Property
{
    public class PropertyRequestDTO
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio por noche es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio por noche debe ser mayor que cero.")]
        public decimal NightlyPrice { get; set; }

        [Required(ErrorMessage = "La capacidad de huéspedes es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad de huéspedes debe ser al menos 1.")]
        public int MaxGuests { get; set; }

        [Required(ErrorMessage = "La cantidad de habitaciones es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de habitaciones debe ser un número positivio.")]
        public int Bedrooms { get; set; }

        [Required(ErrorMessage = "La cantidad de baños es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de baños debe ser un número positivio.")]
        public int Bathrooms { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        public AddressDTO Address { get; set; } = new AddressDTO();

        [MinLength(0, ErrorMessage = "La descripción debe tener al menos 0 caracteres.")]
        [MaxLength(3000, ErrorMessage = "La descripción no puede exceder los 3000 caracteres.")]
        public string Description { get; set; } = string.Empty;

        // Ver cómo tomarlo del usuario que está creando la propiedad.
        public int OwnerId { get; set; } 

        // Luego vemos como manejar los Amenities, me imagino que en el fron podamos acceder a la lista de amenities y pasar el id para hacer el request
        public ICollection<int> AmenityIds { get; set; } = new List<int>();

        // Acá me imagino que en el formulario de propiedades, al cargar una imagen se crea la misma y nos deja el id para hacer el request y asignarlo
        public ICollection<int> ImageIds { get; set; } = new List<int>();

    }
}
