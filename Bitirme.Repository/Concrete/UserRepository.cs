using Bitirme.Core.Models;
using Bitirme.Repository.Abstract;
using Bitirme.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bitirme.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}