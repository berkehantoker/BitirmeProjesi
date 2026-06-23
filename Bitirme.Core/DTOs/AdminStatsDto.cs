namespace Bitirme.Core.DTOs
{
    public class AdminStatsDto
    {
        public int TotalUsers { get; set; }           
        public int TotalTestsCompleted { get; set; }  
        public double AverageScoreA { get; set; }    
        public double AverageScoreB { get; set; }    
        public double AverageScoreC { get; set; }     
    }
}