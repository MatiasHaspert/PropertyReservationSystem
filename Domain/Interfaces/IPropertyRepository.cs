using PropertyReservation.Domain.Entities;

namespace PropertyReservation.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(int id);
        Task<Property> AddAsync(Property property);

        Task UpdateAsync(Property property);
        Task DeleteAsync(int id);
        bool Exists(int id);

    }
}
