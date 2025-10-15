using Microsoft.EntityFrameworkCore;
using ReservaPropiedades.Domain.Entities;
using ReservaPropiedades.Domain.Interfaces;
using ReservaPropiedades.Infrastructure.Data;

namespace ReservaPropiedades.Infrastructure.Repositories
{
    public class ReseñaRepository : IReseñaRepository
    {
        private readonly AppDbContext _context;

        public ReseñaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Reseña>> GetReseñasForProperty(int propiedadId)
        {
            var reseñas = await _context.Reseñas
                .Where(r => r.PropiedadId == propiedadId)
                .ToListAsync();

            return reseñas;
        }

    }
}
