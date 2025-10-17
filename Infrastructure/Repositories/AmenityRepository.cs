using PropertyReservation.Domain.Interfaces;
using PropertyReservation.Infrastructure.Data;
using PropertyReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PropertyReservation.Infrastructure.Repositories
{
    public class AmenityRespository : IAmenityRepository
    {
        private readonly AppDbContext _context;
        public AmenityRespository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AmenityExistsAsync(int amenityId)
        {
            return await _context.Amenities.AnyAsync(a => a.Id == amenityId);
        }

        public async Task<Amenity> CreateAmenityAsync(Amenity amenity)
        {
            await _context.Amenities.AddAsync(amenity);
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task DeleteAsync(int amenityId)
        {
            var amenity = await _context.Amenities.FindAsync(amenityId);
            if (amenity != null)
            {
                _context.Amenities.Remove(amenity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Amenity>> GetAllAmenitiesAsync()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task UpdateAmenityAsync(Amenity amenity)
        {
            _context.Amenities.Update(amenity);
            await _context.SaveChangesAsync();
        }
    }
}
