using Bitirme.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BitirmeProjesi.Controllers
{
    // Şimdilik sadece giriş yapmışlar görebilir. (İleride Role="Admin" ekleyebiliriz)
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var stats = await _adminService.GetSystemStatisticsAsync();
            return Ok(stats);
        }
    }
}