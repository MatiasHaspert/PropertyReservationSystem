using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Domain.ValueObjects;

namespace Backend.Infrastructure.Data
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

            // Datos iniciales de servicios populares
            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { Id = 1, Name = "Wi-Fi" },
                new Amenity { Id = 2, Name = "Aire acondicionado" },
                new Amenity { Id = 3, Name = "Calefacción" },
                new Amenity { Id = 4, Name = "Televisor" },
                new Amenity { Id = 5, Name = "Cocina equipada" },
                new Amenity { Id = 6, Name = "Heladera" },
                new Amenity { Id = 7, Name = "Microondas" },
                new Amenity { Id = 8, Name = "Lavarropas" },
                new Amenity { Id = 9, Name = "Piscina" },
                new Amenity { Id = 10, Name = "Estacionamiento gratuito" },
                new Amenity { Id = 11, Name = "Gimnasio" },
                new Amenity { Id = 12, Name = "Jacuzzi" },
                new Amenity { Id = 13, Name = "Balcón o terraza" },
                new Amenity { Id = 14, Name = "Vista al mar" },
                new Amenity { Id = 15, Name = "Parrilla" },
                new Amenity { Id = 16, Name = "Mascotas permitidas" },
                new Amenity { Id = 17, Name = "Desayuno incluido" },
                new Amenity { Id = 18, Name = "Acceso a playa" },
                new Amenity { Id = 19, Name = "Caja fuerte" },
                new Amenity { Id = 20, Name = "Cámaras de seguridad" }
            );

        }

    }
}
