using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BitirmeProjesi.Data;
using BitirmeProjesi.Models;
using BitirmeProjesi.Services;
using System.Security.Claims;
using Bitirme.Core.DTOs;

namespace BitirmeProjesi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestResultController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AiService _aiService;

        public TestResultController(ApplicationDbContext context, AiService aiService)
        {
            _context = context;
            _aiService = aiService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] List<QuestionResponseDto> responses)
        {

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();
            int userId = int.Parse(userIdString);

            var questions = await _context.Questions.ToListAsync();


            var detailedResponsesForAi = new List<string>();
            var dimensionScores = new Dictionary<string, double> { { "A", 0 }, { "B", 0 }, { "C", 0 } };
            var dimensionCounts = new Dictionary<string, int> { { "A", 0 }, { "B", 0 }, { "C", 0 } };


            foreach (var resp in responses)
            {
                var question = questions.FirstOrDefault(q => q.Id == resp.QuestionId);
                if (question != null)
                {

                    double calculatedScore = question.IsReverse ? (6 - resp.AnswerValue) : resp.AnswerValue;

                    dimensionScores[question.Dimension] += calculatedScore;
                    dimensionCounts[question.Dimension]++;


                    detailedResponsesForAi.Add($"- Soru: {question.QuestionText} | Puan: {resp.AnswerValue}/5");


                    _context.UserResponses.Add(new UserResponse
                    {
                        UserId = userId,
                        QuestionId = resp.QuestionId,
                        AnswerValue = resp.AnswerValue,
                        CreatedAt = DateTime.Now
                    });
                }
            }


            double GetPercentage(string dim) => dimensionCounts[dim] > 0
                ? (dimensionScores[dim] / (dimensionCounts[dim] * 5)) * 100
                : 0;

            double pA = GetPercentage("A");
            double pB = GetPercentage("B");
            double pC = GetPercentage("C");


            string aiReport = await _aiService.GenerateDetailedReportAsync(pA, pB, pC, detailedResponsesForAi);


            var finalResultRecord = new TestResult
            {
                UserId = userId,
                ScoreA = pA,
                ScoreB = pB,
                ScoreC = pC,
                AiReport = aiReport,
                CreatedAt = DateTime.Now
            };
            _context.TestResults.Add(finalResultRecord);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Analiz başarıyla tamamlandı ve kaydedildi.",
                Analysis = new[] {
                    new { Dimension = "A", Percentage = Math.Round(pA, 2) },
                    new { Dimension = "B", Percentage = Math.Round(pB, 2) },
                    new { Dimension = "C", Percentage = Math.Round(pC, 2) }
                },
                AiReport = aiReport
            });
        }
    }
}