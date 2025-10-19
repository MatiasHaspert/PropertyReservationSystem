using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAllAmenitiesAsync();
        Task<ICollection<Amenity>> GetAmenitiesByIdsAsync(ICollection<int> amenityIds);
        Task<Amenity> CreateAmenityAsync(Amenity amenity);
        Task UpdateAmenityAsync(Amenity amenity);
        Task DeleteAsync(int amenityId);
        Task<bool> AmenityExistsAsync(int amenityId);
    }
}
