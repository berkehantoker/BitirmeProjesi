using Bitirme.Core.Models;

namespace Bitirme.Repository.Abstract
{
    public interface ITestRepository
    {
        Task<List<Question>> GetAllQuestions();
        Task SaveTestResult(List<UserResponse> responses, TestResult result);

        Task<List<TestResult>> GetUserHistoryAsync(int userId);

        Task<TestResult> GetLastTestByUserIdAsync(int userId);
    }
}