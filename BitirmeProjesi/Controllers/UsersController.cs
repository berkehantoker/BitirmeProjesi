using Bitirme.Core.DTOs;
using Bitirme.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BitirmeProjesi.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile-info")]
        public async Task<IActionResult> GetProfileInfo()
        {
            // Token'ın içindeki (Claim) kullanıcı ID'sini okuyoruz
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            int userId = int.Parse(userIdString);
            var profileData = await _userService.GetUserProfileAsync(userId);

            if (profileData == null) return NotFound("Kullanıcı bulunamadı.");

            return Ok(profileData);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            int userId = int.Parse(userIdString);
            var result = await _userService.UpdateProfileAsync(userId, dto);

            if (!result) return BadRequest("Profil güncellenirken bir hata oluştu.");

            return Ok(new { message = "Profil başarıyla güncellendi." });
        }
    }
}