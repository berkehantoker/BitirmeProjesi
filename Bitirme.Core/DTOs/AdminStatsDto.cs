using System;
using System.Collections.Generic;

namespace Bitirme.Core.DTOs
{
    public class AdminStatsDto
    {
        public int TotalUsers { get; set; }
        public int TotalTestsCompleted { get; set; }
        public double AverageScoreA { get; set; }
        public double AverageScoreB { get; set; }
        public double AverageScoreC { get; set; }

        // Pasta Grafiği İçin
        public int HighAwarenessCount { get; set; } // Yüksek Farkındalık (Güvenli)
        public int MediumAwarenessCount { get; set; } // Orta Seviye
        public int RiskCount { get; set; } // Riskli Grup

        // Son Aktiviteler İçin
        public List<RecentTestDto> RecentTests { get; set; } = new List<RecentTestDto>();
    }

    public class RecentTestDto
    {
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public double AverageScore { get; set; }
    }
}