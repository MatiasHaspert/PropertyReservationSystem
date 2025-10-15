namespace PropertyReservation.Domain.ValueObjects
{
    public class Address
    {
        public string City { get; set; } 
        public string Country { get; set; }
        public string StreetAddress { get; set; }
        public int PostalCode { get; set; }
        public string State { get; set; }

        public Address() 
        {
            City = string.Empty;
            Country = string.Empty;
            StreetAddress = string.Empty;
            PostalCode = 0;
            State = string.Empty;
        }
        public Address(string city, string country, string streetAddress, int postalCode, string state)
        {
            City = city;
            Country = country;
            StreetAddress = streetAddress;
            PostalCode = postalCode;
            State = state;
        }

    }
}
