using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Data;

namespace Backend.Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly AppDbContext _context;
        public PropertyImageRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<PropertyImage?> GetPropertyImageByIdAsync(int imageId)
        {
            return await _context.PropertyImages.FindAsync(imageId);
        }

        public async Task<ICollection<PropertyImage>> GetPropertyImagesByIdsAsync(ICollection<int> imageIds)
        {
            return await _context.PropertyImages
                .Where(img => imageIds.Contains(img.Id))
                .ToListAsync();
        }

        public async Task<ICollection<PropertyImage>> GetPropertyImagesByPropertyIdAsync(int propertyId)
        {
            return await _context.PropertyImages
                .Where(img => img.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task AddRangePropertyImageAsync(IEnumerable<PropertyImage> images)
        {
            await _context.PropertyImages.AddRangeAsync(images);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<PropertyImage> images)
        {
            _context.PropertyImages.UpdateRange(images);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePropertyImageAsync(PropertyImage image)
        {
            var existingImage = await _context.PropertyImages.FindAsync(image.Id);
            if (existingImage != null)
            {
                _context.PropertyImages.Remove(existingImage);
                await _context.SaveChangesAsync();
            }
        }

    }
}
