using Microsoft.EntityFrameworkCore;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Domain.Interfaces;
using PropertyReservation.Infrastructure.Data;
using System.Threading.Tasks;

namespace PropertyReservation.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<User?> GetByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
