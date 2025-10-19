using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task<PropertyImage?> GetPropertyImageByIdAsync(int imageId);
        Task<ICollection<PropertyImage>> GetPropertyImagesByIdsAsync(ICollection<int> imageIds);
        Task<ICollection<PropertyImage>> GetPropertyImagesByPropertyIdAsync(int propertyId);
        Task AddRangePropertyImageAsync(IEnumerable<PropertyImage> images);
        Task UpdateRangeAsync(IEnumerable<PropertyImage> images);
        Task DeletePropertyImageAsync(PropertyImage image);
    }
}
