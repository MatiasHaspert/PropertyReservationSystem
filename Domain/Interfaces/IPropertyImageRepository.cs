using PropertyReservation.Domain.Entities;

namespace PropertyReservation.Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task AddRangePropertyImageAsync(IEnumerable<PropertyImage> images);
        Task<IEnumerable<PropertyImage>> GetPropertyImagesByPropertyIdAsync(int propertyId);
        Task DeletePropertyImageAsync(PropertyImage image);

        Task<PropertyImage?> GetPropertyImageByIdAsync(int imageId);

    }
}
