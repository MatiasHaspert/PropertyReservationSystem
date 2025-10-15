using Microsoft.EntityFrameworkCore;
using ReservaPropiedades.Domain.Entities;
using ReservaPropiedades.Domain.ValueObjects;

namespace ReservaPropiedades.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<ImagenPropiedad> ImagenesPropiedades { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<DisponibilidadPropiedad> DisponibilidadesPropiedades { get; set; }
        public DbSet<Reseña> Reseñas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<Propiedad>().OwnsOne(p => p.Ubicacion);
            modelBuilder.Entity<Usuario>().OwnsOne(u => u.Ubicacion);

            modelBuilder.Entity<Propiedad>().HasData(
                new
                {
                    Id = 1,
                    Titulo = "Casa de prueba en la playa",
                    Descripcion = "Hermosa casa frente al mar con pileta y parrilla.",
                    PrecioPorNoche = 15000m,
                    CapacidadHuespedes = 4,
                    NumeroHabitaciones = 2,
                    NumeroBaños = 1,
                    UsuarioId = 1,
                    Ubicacion_Ciudad = "Mar del Plata",
                    Ubicacion_Provincia = "Buenos Aires",
                    Ubicacion_Pais = "Argentina",
                    Ubicacion_Direccion = "Av. Costanera 1234"
                }
            );

            // Índice único filtrado: solo una imagen principal por propiedad
            modelBuilder.Entity<ImagenPropiedad>()
                .HasIndex(i => new { i.PropiedadId, i.EsImagenPrincipal })
                .IsUnique()
                .HasFilter("[EsImagenPrincipal] = 1");

            // Configurar la relación entre Reseña y Usuario para evitar eliminación en cascada
            modelBuilder.Entity<Reseña>()
                        .HasOne(r => r.Usuario)
                        .WithMany(u => u.Reseñas)
                        .HasForeignKey(r => r.UsuarioId)
                        .OnDelete(DeleteBehavior.Restrict); // o DeleteBehavior.NoAction
            
            base.OnModelCreating(modelBuilder);

        }

    }
}
