using Backend.Domain.Entities;

namespace Backend.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int userId);
    }
}
