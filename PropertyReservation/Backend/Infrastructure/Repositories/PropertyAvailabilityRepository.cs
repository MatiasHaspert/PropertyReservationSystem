using Humanizer;
using Microsoft.EntityFrameworkCore;
using Backend.Application.DTOs.PropertyAvailability;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;

namespace Backend.Infrastructure.Repositories
{
    public class PropertyAvailabilityRepository : IPropertyAvailabilityRespository
    {
        private readonly AppDbContext _context;

        public PropertyAvailabilityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PropertyAvailability>> GetPropertyAvailabilitiesAsync(int propertyId)
        {
            return await _context.PropertyAvailabilities
                .Where(pa => pa.PropertyId == propertyId)
                .ToListAsync();
        }
        
        public async Task<PropertyAvailability> CreatePropertyAvailabilityAsync(PropertyAvailability availability)
        {
            await _context.PropertyAvailabilities.AddAsync(availability);
            await _context.SaveChangesAsync();
            return availability;
        }

        public async Task UpdatePropertyAvailabilityAsync(PropertyAvailability availability)
        {
            _context.Update(availability);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePropertyAvailabilityAsync(int availabilityId)
        {
            var availability = await _context.PropertyAvailabilities.FindAsync(availabilityId);
            if (availability != null)
            {
                _context.PropertyAvailabilities.Remove(availability);
                _context.SaveChanges();
            }
        }

        public async Task<bool> HasOverlappingAvailabilityAsync(PropertyAvailabilityRequestDTO dto, int? excludeAvailabilityId = null)
        {
            // Todos intervalos [A, B] y [C, D] se solapan si A <= D y B >= C
            return await _context.PropertyAvailabilities
                .AnyAsync(a =>
                    a.PropertyId == dto.PropertyId &&
                    (excludeAvailabilityId == null || a.Id != excludeAvailabilityId) && // Excluir solo si se pasa
                    a.StartDate <= dto.EndDate &&
                    a.EndDate >= dto.StartDate);
        }


        public async Task<bool> PropertyAvailabilityExistsAsync(int availabilityId)
        {
            return await _context.PropertyAvailabilities.AnyAsync(a => a.Id == availabilityId);
        }

        public async Task<PropertyAvailability?> GetByIdAsync(int availabilityId)
        {
            return await _context.PropertyAvailabilities.FindAsync(availabilityId);
        }
    }
}
