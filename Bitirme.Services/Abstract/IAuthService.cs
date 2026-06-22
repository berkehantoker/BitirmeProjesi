using System.Threading.Tasks;
using Bitirme.Core.DTOs;
using Bitirme.Core.Models;

namespace Bitirme.Services.Abstract
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserRegisterDto request);
        Task<string> LoginAsync(UserLoginDto request);
    }
}