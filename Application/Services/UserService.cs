using AutoMapper;
using PropertyReservation.Application.Interfaces;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.Interfaces;

namespace PropertyReservation.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return  await _UserRepository.GetByIdAsync(userId);
        }
    }
}
