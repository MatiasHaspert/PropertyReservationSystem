using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;
using Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;

        public PropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Property>> GetAllWithDetailsAsync()
        {
            return await _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                .ToListAsync();
        }

        public async Task<Property?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User) 
                .Include(p => p.Amenities)
                .FirstOrDefaultAsync(p => p.Id == id);
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

        public async Task<bool> IsWithinAvailabilityRangeAsync(int propertyId, DateTime startDate, DateTime endDate)
        {
            return await _context.PropertyAvailabilities
                .AnyAsync(a =>
                    a.PropertyId == propertyId &&
                    a.StartDate <= startDate &&
                    a.EndDate >= endDate
                );
        }

    }
}
