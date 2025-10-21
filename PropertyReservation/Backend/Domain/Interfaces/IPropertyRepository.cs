using Backend.Domain.Entities;

namespace Backend.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllWithDetailsAsync();
        Task<Property?> GetByIdWithDetailsAsync(int id);
        Task<Property?> GetByIdAsync(int id);
        Task<Property> AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(int id);
        Task<bool> PropertyExistsAsync(int id);
        Task<bool> IsWithinAvailabilityRangeAsync(int propertyId, DateTime startDate, DateTime endDate);
    }
}
