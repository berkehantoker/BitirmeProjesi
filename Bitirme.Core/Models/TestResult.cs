using System;
using System.ComponentModel.DataAnnotations;

namespace Bitirme.Core.Models
{
    public class TestResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public double ScoreA { get; set; } 
        public double ScoreB { get; set; } 
        public double ScoreC { get; set; } 


        [Required]
        public string AiReport { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User User { get; set; }
    }
}