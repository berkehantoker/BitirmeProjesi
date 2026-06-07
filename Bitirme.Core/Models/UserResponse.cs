namespace BitirmeProjesi.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int QuestionId { get; set; }
        public int AnswerValue { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public Question Question { get; set; }
        public User User { get; set; }




    }
}
