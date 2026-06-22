using Bitirme.Core.Models;
using Bitirme.Repository.Abstract;
using Bitirme.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Bitirme.Repository.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthRepository(ApplicationDbContext context) => _context = context;

        public async Task<User?> GetUserByEmail(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}