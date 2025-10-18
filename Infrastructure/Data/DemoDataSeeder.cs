using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace PropertyReservation.Infrastructure.Data
{
    public class DemoDataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Eliminar usuarios demo existentes
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Name = 'Owner1'");

            // Crear usuarios demo
            var owner1 = new User
            {
                Name = "Owner1",
                LastName = "Owner1",
                Email = "owner1@example.com",
                Phone = "1234567890",
                Address = new Address
                {
                    City = "Ciudad",
                    Country = "Argentina",
                    StreetAddress = "Calle Falsa 123",
                    PostalCode = 1000,
                    State = "Buenos Aires"
                }
            };
            context.Users.Add(owner1);
            await context.SaveChangesAsync();

            // Eliminamos propiedades para recargar los datos
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Properties WHERE Title = 'Owner1'");

            context.Properties.Add(new Property
            {
                Title = "Casa Alpina",
                
                NightlyPrice = 120000,
                MaxGuests = 3,
                Bedrooms = 3,
                Bathrooms = 2,
                Address = new Address
                {
                    Country = "Argentina",
                    City = "Rosario",
                    PostalCode = 2000,
                    StreetAddress = "Calle Montaña 123",
                    State = "Santa Fe"
                },
                Description = """
				Esta encantadora casa alpina combina estilo rústico y confort moderno.
				Ideal para familias, ofrece un amplio jardín 🌳 y un entorno tranquilo.
	
				🏔️ Ubicada en una zona elevada con aire puro y vistas despejadas.
				🔥 Incluye estufa a leña y amplios ventanales que aportan luz natural.
				""",
                OwnerId = owner1.Id,
            });
            await context.SaveChangesAsync();
        }
    }
}
