using Bitirme.Core.Models;
using Bitirme.Repository.Abstract;
using Bitirme.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Bitirme.Repository.Concrete
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<List<TestResult>> GetUserHistoryAsync(int userId)
        {
            return await _context.TestResults
                .Where(tr => tr.UserId == userId)
                .OrderByDescending(tr => tr.Id)
                .ToListAsync();
        }

        public async Task SaveTestResult(List<UserResponse> responses, TestResult result)
        {
            await _context.UserResponses.AddRangeAsync(responses);
            await _context.TestResults.AddAsync(result);
            await _context.SaveChangesAsync();
        }


        public async Task<TestResult?> GetLastTestByUserIdAsync(int userId)
        {
            return await _context.TestResults
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}