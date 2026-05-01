namespace BitirmeProjesi.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } 
        public string Dimension { get; set; } 
        public bool IsReverse { get; set; } 
    }
}
