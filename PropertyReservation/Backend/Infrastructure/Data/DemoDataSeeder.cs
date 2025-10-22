using Backend.Domain.Entities;
using Backend.Domain.Enums;
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
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Properties WHERE Title = 'Departamento Centro'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Properties WHERE Title = 'Chalet Playa'");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Properties WHERE Title = 'Finca'");

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
            var property1 = new Property
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
            context.Properties.Add(property1);
            await context.SaveChangesAsync();

            var property2 = new Property
            {
                Title = "Departamento Centro",
                NightlyPrice = 80000,
                MaxGuests = 4,
                Bedrooms = 2,
                Bathrooms = 1,
                Address = new Address(
                    country: "Argentina",
                    state: "Córdoba",
                    city: "Córdoba",
                    postalCode: 5000,
                    streetAddress: "Av. Principal 456"
                ),
                Description = """
                    Departamento moderno en pleno centro de Córdoba. Perfecto para estudiantes o parejas.
 
                    🏙️ Cercano a universidades, comercios y transporte público.
                    🛋️ Ambientes luminosos con excelente distribución y balcón al frente.
                    """,
                OwnerId = owner2.Id
            };
 
            var property3 = new Property
            {
                Title = "Chalet Playa",
                NightlyPrice = 200000,
                MaxGuests = 5,
                Bedrooms = 4,
                Bathrooms = 3,
                Address = new Address(
                    country: "Argentina",
                    state: "Buenos Aires",
                    city: "Mar del Plata",
                    postalCode: 7600,
                    streetAddress: "Camino Costero 789"
                ),
                Description = """
                    Chalet frente al mar ideal para vacaciones o vivienda permanente.
 
                    🌊 Vista directa al océano desde el living.
                    🍃 Cuenta con jardín, garaje y parrilla.
                    ✨ Ambientes amplios y luminosos, a pasos de la playa.
                    """,
                OwnerId = owner1.Id
            };
 
            var property4 = new Property
            {
                Title = "Finca",
                NightlyPrice = 150000,
                MaxGuests = 6,
                Bedrooms = 3,
                Bathrooms = 2,
                Address = new Address(
                    country: "Argentina",
                    state: "Mendoza",
                    city: "Mendoza",
                    postalCode: 5500,
                    streetAddress: "Camino de los Vinos 789"
                ),
                Description = """
                    Finca en una exclusiva zona vitivinícola de Mendoza.
 
                    🍇 Entorno natural con viñedos cercanos.
                    🏡 Estilo rústico y elegante, ideal para inversión turística.
                    🔒 Propiedad ya vendida, no visible para usuarios públicos.
                    """,
                OwnerId = owner2.Id
            };
 
            context.Properties.AddRange(property2, property3, property4);
            await context.SaveChangesAsync();


            // Agregar reseñas
            var reviews = new List<Review>
            {
                new Review
                {
                    PropertyId = property1.Id,
                    UserId = owner1.Id,
                    Rating = 5,
                    Comment = "Excelente lugar, muy limpio y con una vista increíble.",
                    Date = DateTime.UtcNow.AddDays(-10)
                },
                new Review
                {
                    PropertyId = property1.Id,
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
                    PropertyId = property1.Id,
                    StartDate = new DateTime(2025, 10, 10),
                    EndDate = new DateTime(2025, 10, 20)
                },
                new PropertyAvailability
                {
                    PropertyId = property1.Id,
                    StartDate = new DateTime(2025, 11, 1),
                    EndDate = new DateTime(2025, 11, 15)
                }
            };
            context.PropertyAvailabilities.AddRange(availabilities);

           
            // Buscamos servicios ya cargados
            var wifi = await context.Amenities.FirstOrDefaultAsync(s => s.Name == "Wi-Fi");
            var gimnasio = await context.Amenities.FirstOrDefaultAsync(s => s.Name == "Gimnasio");

            // Asociamos servicios existentes a la propiedad
            if (wifi != null) property1.Amenities.Add(wifi);
            if (gimnasio != null) property1.Amenities.Add(gimnasio);

            // Guardar todo
            await context.SaveChangesAsync();

            // Agregamos reservas a la propiedad1
            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    PropertyId = property1.Id,
                    GuestId = owner2.Id,
                    StartDate = new DateTime(2025, 10, 10),
                    EndDate = new DateTime(2025, 10, 15),
                    TotalGuests = 2,
                    TotalPrice = property1.NightlyPrice * 2,
                    Status = ReservationStatus.Pending,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Reservation
                {
                    PropertyId = property1.Id,
                    GuestId = owner2.Id,
                    StartDate = new DateTime(2025, 11, 05),
                    EndDate = new DateTime(2025, 11, 08),
                    TotalGuests = 4,
                    TotalPrice = property1.NightlyPrice * 4,
                    Status = ReservationStatus.Pending,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                }
            };
            context.Reservations.AddRange(reservations);

            await context.SaveChangesAsync();
        }
    }
}
