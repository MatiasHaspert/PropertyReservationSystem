using Backend.Domain.Entities;
using Backend.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Data
{
    public class DemoDataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Eliminar datos anteriores
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Name = 'Owner1'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Name = 'Owner2'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Properties WHERE Title = 'Casa Alpina'");

            // Crear usuario demo
            var owner1 = new User
            {
                Name = "Owner1",
                LastName = "Owner1",
                Email = "owner1@example.com",
                Phone = "1234567890",
                Address = new Address(
                    country: "Argentina",
                    state: "Buenos Aires",
                    city: "Ciudad",
                    postalCode: 1000,
                    streetAddress: "Calle Falsa 123"
                )
            };
            context.Users.Add(owner1);
            await context.SaveChangesAsync();

            var owner2 = new User
            {
                Name = "Owner2",
                LastName = "Owner2",
                Email = "owner2@example.com",
                Phone = "123117890",
                Address = new Address(
                    country: "Argentina",
                    state: "Santa fe",
                    city: "Rosario",
                    postalCode: 2000,
                    streetAddress: "Calle Falsa 123444"
                )
            };
            context.Users.Add(owner2);
            await context.SaveChangesAsync();

            // Crear propiedad demo
            var property = new Property
            {
                Title = "Casa Alpina",
                NightlyPrice = 120000,
                MaxGuests = 3,
                Bedrooms = 3,
                Bathrooms = 2,
                Address = new Address(
                    country: "Argentina",
                    state: "Santa Fe",
                    city: "Rosario",
                    postalCode: 2000,
                    streetAddress: "Calle Montaña 123"
                ),
                Description = """
                    Esta encantadora casa alpina combina estilo rústico y confort moderno.
                    Ideal para familias, ofrece un amplio jardín 🌳 y un entorno tranquilo.

                    🏔️ Ubicada en una zona elevada con aire puro y vistas despejadas.
                    🔥 Incluye estufa a leña y amplios ventanales que aportan luz natural.
                    """,
                OwnerId = owner1.Id
            };
            context.Properties.Add(property);
            await context.SaveChangesAsync();

            // Agregar servicios a la propiedad 
            

            // Agregar reseñas
            var reviews = new List<Review>
            {
                new Review
                {
                    PropertyId = property.Id,
                    UserId = owner1.Id,
                    Rating = 5,
                    Comment = "Excelente lugar, muy limpio y con una vista increíble.",
                    Date = DateTime.UtcNow.AddDays(-10)
                },
                new Review
                {
                    PropertyId = property.Id,
                    UserId = owner2.Id,
                    Rating = 4,
                    Comment = "Hermosa casa, aunque un poco lejos del centro.",
                    Date = DateTime.UtcNow.AddDays(-3)
                }
            };
            context.Reviews.AddRange(reviews);

            // Agregar disponibilidades
            var availabilities = new List<PropertyAvailability>
            {
                new PropertyAvailability
                {
                    PropertyId = property.Id,
                    StartDate = new DateTime(2025, 10, 10),
                    EndDate = new DateTime(2025, 10, 20)
                },
                new PropertyAvailability
                {
                    PropertyId = property.Id,
                    StartDate = new DateTime(2025, 11, 1),
                    EndDate = new DateTime(2025, 11, 15)
                }
            };
            context.PropertyAvailabilities.AddRange(availabilities);

           
            // Buscamos servicios ya cargados
            var wifi = await context.Amenities.FirstOrDefaultAsync(s => s.Name == "Wi-Fi");
            var gimnasio = await context.Amenities.FirstOrDefaultAsync(s => s.Name == "Gimnasio");

            // Asociamos servicios existentes a la propiedad
            if (wifi != null) property.Amenities.Add(wifi);
            if (gimnasio != null) property.Amenities.Add(gimnasio);

            // Guardar todo
            await context.SaveChangesAsync();
        }
    }
}
