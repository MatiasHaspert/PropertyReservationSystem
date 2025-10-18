using PropertyReservation.Domain.Entities;

namespace PropertyReservation.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
    }
}
