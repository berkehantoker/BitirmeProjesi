using Bitirme.Core.DTOs;
using Bitirme.Repository.Abstract;
using Bitirme.Services.Abstract;
using System.Threading.Tasks;

namespace Bitirme.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        // EKLENEN YENİ GÜNCELLEME MANTIĞI
        public async Task<bool> UpdateProfileAsync(int userId, UpdateProfileDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            // Kullanıcı adını güncelle
            if (!string.IsNullOrEmpty(dto.NewUsername))
            {
                user.Username = dto.NewUsername;
            }

            // Eğer yeni şifre gönderildiyse şifre alanını güncelle
            if (!string.IsNullOrEmpty(dto.NewPassword))
            {
                
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
             
            }

           
            return await _userRepository.SaveChangesAsync();
        }
    }
}