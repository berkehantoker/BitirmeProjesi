using Bitirme.Services.Abstract;
using Bitirme.Core.DTOs;
using Bitirme.Core.Models;
using Bitirme.Repository.Abstract;


namespace Bitirme.Business.Concrete
{
    public class TestManager : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IAiService _aiService;

        public TestManager(ITestRepository testRepository, IAiService aiService)
        {
            _testRepository = testRepository;
            _aiService = aiService;
        }

        public async Task<List<TestResult>> GetUserHistoryAsync(int userId)
        {
            var userHistory = await _testRepository.GetUserHistoryAsync(userId);

            return userHistory;
        }

        public async Task<TestAnalysisResultDto> SubmitTestAsync(int userId, List<QuestionResponseDto> responses)
        {
            var questions = await _testRepository.GetAllQuestions();

            var userResponses = new List<UserResponse>();
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

                    userResponses.Add(new UserResponse
                    {
                        UserId = userId,
                        QuestionId = resp.QuestionId,
                        AnswerValue = resp.AnswerValue,
                        CreatedAt = DateTime.Now
                    });
                }
            }

            double pA = dimensionCounts["A"] > 0 ? (dimensionScores["A"] / (dimensionCounts["A"] * 5)) * 100 : 0;
            double pB = dimensionCounts["B"] > 0 ? (dimensionScores["B"] / (dimensionCounts["B"] * 5)) * 100 : 0;
            double pC = dimensionCounts["C"] > 0 ? (dimensionScores["C"] / (dimensionCounts["C"] * 5)) * 100 : 0;

            string aiReport = await _aiService.GenerateDetailedReportAsync(pA, pB, pC, detailedResponsesForAi);

            var finalResult = new TestResult
            {
                UserId = userId,
                ScoreA = Math.Round(pA, 2),
                ScoreB = Math.Round(pB, 2),
                ScoreC = Math.Round(pC, 2),
                AiReport = aiReport,
                CreatedAt = DateTime.Now
            };

            await _testRepository.SaveTestResult(userResponses, finalResult);

            return new TestAnalysisResultDto
            {
                Message = "Analiz başarıyla tamamlandı.",
                Analysis = new List<DimensionScoreDto> {
                    new() { Dimension = "A", Percentage = Math.Round(pA, 2) },
                    new() { Dimension = "B", Percentage = Math.Round(pB, 2) },
                    new() { Dimension = "C", Percentage = Math.Round(pC, 2) }
                },
                AiReport = aiReport
            };
        }
    }
}