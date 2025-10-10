using ReservaPropiedades.Domain.Interfaces;
using ReservaPropiedades.Infrastructure.Data;

namespace ReservaPropiedades.Infrastructure.Repositories
{
    public class PropiedadRepository : IPropiedadRepository
    {
        private readonly AppDbContext _context;

        public PropiedadRepository(AppDbContext context)
        {
            _context = context;
        }


    }
}
