using Bitirme.Core.Models;

namespace Bitirme.Repository.Abstract
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task AddUser(User user);
    }
}