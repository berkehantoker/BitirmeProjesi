using Bitirme.Core.DTOs;
using Bitirme.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitirme.Services.Abstract
{
    public interface ITestService
    {
        Task<TestAnalysisResultDto> SubmitTestAsync(int userId, List<QuestionResponseDto> responses);

        Task<List<TestResult>> GetUserHistoryAsync(int userId);

        Task<TestEligibilityDto> CheckTestEligibilityAsync(int userId);
    }
}