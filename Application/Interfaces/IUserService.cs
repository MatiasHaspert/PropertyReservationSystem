using PropertyReservation.Domain.Entities;

namespace PropertyReservation.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int userId);
    }
}
