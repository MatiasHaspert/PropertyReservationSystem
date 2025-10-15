using Microsoft.EntityFrameworkCore;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.ValueObjects;

namespace PropertyReservation.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<PropertyAvailability> PropertyAvailabilities { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<Property>().OwnsOne(p => p.Address);
            modelBuilder.Entity<User>().OwnsOne(u => u.Address);

            /* modelBuilder.Entity<Property>().HasData(
                new
                {
                    Title = "Casa de prueba en la playa",
                    Description = "Hermosa casa frente al mar con pileta y parrilla.",
                    NightlyPrice = 15000m,
                    MaxGuests = 4,
                    Bedrooms = 2,
                    Bathrooms = 1,
                    UserId = 1,
                    Address_City = "Mar del Plata",
                    Address_State = "Buenos Aires",
                    Address_Country = "Argentina",
                    Address_StreetAddress = "Av. Costanera 1234"
                }
            );*/

            // Índice único filtrado: solo una imagen principal por property
            modelBuilder.Entity<PropertyImage>()
                .HasIndex(i => new { i.PropertyId, i.IsMainImage })
                .IsUnique()
                .HasFilter("[IsMainImage] = 1");

            // Configurar la relación entre Review y User para evitar eliminación en cascada
            modelBuilder.Entity<Review>()
                        .HasOne(r => r.User)
                        .WithMany(u => u.Reviews)
                        .HasForeignKey(r => r.UserId)
                        .OnDelete(DeleteBehavior.Restrict); // o DeleteBehavior.NoAction
            
            base.OnModelCreating(modelBuilder);

        }

    }
}
