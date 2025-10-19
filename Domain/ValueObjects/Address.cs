using System.ComponentModel.DataAnnotations;

namespace PropertyReservation.Domain.ValueObjects
{
    public class Address
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; } 

        [Required]
        public int PostalCode { get; set; }
        
        [Required]
        public string StreetAddress { get; set; }

        public Address(string country, string state, string city, int postalCode, string streetAddress)
        {
            Country = country;
            State = state;
            City = city;
            PostalCode = postalCode;
            StreetAddress = streetAddress;
        }

    }
}
