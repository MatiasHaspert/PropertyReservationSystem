using ReservaPropiedades.Domain.Interfaces;
using ReservaPropiedades.Infrastructure.Data;
using ReservaPropiedades.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ReservaPropiedades.Infrastructure.Repositories
{
    public class PropiedadRepository : IPropiedadRepository
    {
        private readonly AppDbContext _context;

        public PropiedadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Propiedad>> GetAllAsync()
        {
            return await _context.Propiedades.ToListAsync();
        }

        public async Task<Propiedad> GetByIdAsync(int id)
        {
            return await _context.Propiedades.FindAsync(id);
        }

        public async Task AddAsync(Propiedad propiedad)
        {
            await _context.Propiedades.AddAsync(propiedad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Propiedad propiedad)
        {
            _context.Propiedades.Update(propiedad);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var propiedad = await _context.Propiedades.FindAsync(id);
            if (propiedad != null)
            {
                _context.Propiedades.Remove(propiedad);
                await _context.SaveChangesAsync();
            }
        }
        public bool Exists(int id)
        {
            return _context.Propiedades.Any(e => e.Id == id);
        }
    }
}
