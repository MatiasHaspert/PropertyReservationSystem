using PropertyReservation.Domain.Entities;

namespace PropertyReservation.Application.Interfaces
{
    public interface IPropertyImageService
    {
        Task<List<PropertyImage>> UploadPropertyImagesAsync(int propertyId, List<IFormFile> files, HttpRequest request);
        Task<IEnumerable<PropertyImage>> GetImagesByPropertyAsync(int propertyId);
        Task DeleteImageAsync(int imageId);
    }
}
