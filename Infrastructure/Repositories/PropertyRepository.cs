using PropertyReservation.Domain.Interfaces;
using PropertyReservation.Infrastructure.Data;
using PropertyReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropertyReservation.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;

        public PropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<Property> GetByIdAsync(int id)
        {
            return await _context.Properties.FindAsync(id);
        }

        public async Task<Property> AddAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property != null)
            {
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> PropertyExistsAsync(int id)
        {
            return await _context.Properties.AnyAsync(e => e.Id == id);
        }
    }
}
