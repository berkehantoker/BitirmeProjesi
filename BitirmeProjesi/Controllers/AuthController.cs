using Bitirme.Services.Abstract;
using Bitirme.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BitirmeProjesi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var user = await _authService.RegisterAsync(request);

            
            return Ok(new
            {
                Message = "Kayıt başarılı",
                Username = user.Username,
                Email = user.Email
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var response = await _authService.LoginAsync(request);

            if (response == null)
            {
                return BadRequest("Hatalı email veya şifre.");
            }

            
            return Ok(new
            {
                token = response.Token,
                role = response.Role
            });
        }
    }
}