using Bitirme.Core.Models;
using System.Threading.Tasks;

namespace Bitirme.Repository.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);

        Task<bool> SaveChangesAsync();
    }
}