using Bitirme.Core.DTOs;
using Bitirme.Repository.Abstract;
using Bitirme.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

            // Veri kirliliğini önlemek için her kullanıcının sadece EN SON testini alıyoruz
            var latestTests = await _context.TestResults
                .Include(t => t.User)
                .GroupBy(t => t.UserId)
                .Select(g => g.OrderByDescending(t => t.CreatedAt).FirstOrDefault())
                .ToListAsync();

            int validTestCount = latestTests.Count;
            double avgA = validTestCount > 0 ? latestTests.Average(t => t.ScoreA) : 0;
            double avgB = validTestCount > 0 ? latestTests.Average(t => t.ScoreB) : 0;
            double avgC = validTestCount > 0 ? latestTests.Average(t => t.ScoreC) : 0;

            // Risk Dağılımını Hesapla
            int high = 0, med = 0, risk = 0;
            foreach (var test in latestTests)
            {
                double avg = (test.ScoreA + test.ScoreB + test.ScoreC) / 3.0;
                if (avg >= 80) high++;
                else if (avg >= 50) med++;
                else risk++;
            }

            // Son 5 Test Aktivitesini Çek
            var recentActivities = await _context.TestResults
                .Include(t => t.User)
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .Select(t => new RecentTestDto
                {
                    Username = t.User.Username,
                    Date = t.CreatedAt,
                    AverageScore = Math.Round((t.ScoreA + t.ScoreB + t.ScoreC) / 3.0, 1)
                }).ToListAsync();

            return new AdminStatsDto
            {
                TotalUsers = totalUsers,
                TotalTestsCompleted = totalTests,
                AverageScoreA = Math.Round(avgA, 2),
                AverageScoreB = Math.Round(avgB, 2),
                AverageScoreC = Math.Round(avgC, 2),
                HighAwarenessCount = high,
                MediumAwarenessCount = med,
                RiskCount = risk,
                RecentTests = recentActivities
            };
        }

        // AKADEMİK EXPORT İÇİN YENİ METOT
        public async Task<string> GetExportDataCsvAsync()
        {
            var allTests = await _context.TestResults.Include(t => t.User).OrderBy(t => t.CreatedAt).ToListAsync();

            var csv = "TestId,KullaniciAdi,Tarih,BilisimSistemleriSkoru,KisiselVeriSkoru,GizlilikSkoru\n";
            foreach (var t in allTests)
            {
                csv += $"{t.Id},{t.User.Username},{t.CreatedAt:yyyy-MM-dd HH:mm},{t.ScoreA},{t.ScoreB},{t.ScoreC}\n";
            }
            return csv;
        }
    }
}