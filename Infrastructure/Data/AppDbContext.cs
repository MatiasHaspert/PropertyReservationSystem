using Microsoft.EntityFrameworkCore;
using ReservaPropiedades.Domain.Entities;

namespace ReservaPropiedades.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<ImagenPropiedad> ImagenesPropiedades { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<DisponibilidadPropiedad> DisponibilidadesPropiedades { get; set; }
        public DbSet<Reseña> Reseñas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<Propiedad>().OwnsOne(p => p.Ubicacion);
            base.OnModelCreating(modelBuilder);

        }

    }
}
