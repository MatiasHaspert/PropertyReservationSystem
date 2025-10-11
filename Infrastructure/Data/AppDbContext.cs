using Microsoft.EntityFrameworkCore;
using ReservaPropiedades.Domain.Entities;

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
