using Bitirme.Core.DTOs;
using System.Threading.Tasks;

namespace Bitirme.Services.Abstract
{
    public interface IUserService
    {
        Task<UserDto> GetUserProfileAsync(int userId);

        Task<bool> UpdateProfileAsync(int userId, UpdateProfileDto dto);
    }
}