using Bitirme.Core.DTOs;
using Bitirme.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BitirmeProjesi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestResultController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestResultController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] List<QuestionResponseDto> responses)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            int userId = int.Parse(userIdString);

            var result = await _testService.SubmitTestAsync(userId, responses);

            return Ok(result);
        }


        [Authorize] 
        [HttpGet("my-history")]
        public async Task<IActionResult> GetMyHistory()
        {
            
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }

            int currentUserId = int.Parse(userIdString);

            
            var userHistory = await _testService.GetUserHistoryAsync(currentUserId);

            
            return Ok(userHistory);
        }

    }
}