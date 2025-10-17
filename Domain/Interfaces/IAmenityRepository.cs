using PropertyReservation.Domain.Entities;

namespace PropertyReservation.Domain.Interfaces
{
    public interface IAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAllAmenitiesAsync();
        Task<Amenity> CreateAmenityAsync(Amenity amenity);
        Task UpdateAmenityAsync(Amenity amenity);
        Task DeleteAsync(int amenityId);
        Task<bool> AmenityExistsAsync(int amenityId);
    }
}
