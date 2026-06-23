using Bitirme.Core.DTOs;
using Bitirme.Repository.Abstract;
using Bitirme.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bitirme.Repository.Concrete
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminStatsDto> GetGeneralStatisticsAsync()
        {
            int totalUsers = await _context.Users.CountAsync();
            int totalTests = await _context.TestResults.CountAsync();

            // SİHİRLİ DOKUNUŞ: Her kullanıcının sadece EN SON çözdüğü testini alıyoruz!
            var latestTests = await _context.TestResults
                .GroupBy(t => t.UserId)
                .Select(g => g.OrderByDescending(t => t.CreatedAt).FirstOrDefault())
                .ToListAsync();

            int validTestCount = latestTests.Count;

            double avgA = validTestCount > 0 ? latestTests.Average(t => t.ScoreA) : 0;
            double avgB = validTestCount > 0 ? latestTests.Average(t => t.ScoreB) : 0;
            double avgC = validTestCount > 0 ? latestTests.Average(t => t.ScoreC) : 0;

            return new AdminStatsDto
            {
                TotalUsers = totalUsers,
                TotalTestsCompleted = totalTests, // Toplam çözülen testi yine gösteriyoruz şov için
                AverageScoreA = Math.Round(avgA, 2),
                AverageScoreB = Math.Round(avgB, 2),
                AverageScoreC = Math.Round(avgC, 2)
            };
        }
    }
}