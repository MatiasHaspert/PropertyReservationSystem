using Backend.Application.DTOs.PropertyImage;
using Backend.Domain.Entities;

namespace Backend.Application.Interfaces
{
    public interface IPropertyImageService
    {
        Task<IEnumerable<PropertyImageResponseDTO>> GetImagesByPropertyAsync(int propertyId);
        Task<List<PropertyImageResponseDTO>> UploadPropertyImagesAsync(int propertyId, List<IFormFile> files, HttpRequest request);
        Task DeleteImageAsync(int imageId);
        Task<PropertyImageResponseDTO> SetMainImageAsync(int propertyId, int imageId);
    }
}
