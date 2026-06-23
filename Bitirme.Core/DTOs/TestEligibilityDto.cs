namespace Bitirme.Core.DTOs
{
    public class TestEligibilityDto
    {
        public bool CanTake { get; set; }
        public string Message { get; set; }
        public int HoursLeft { get; set; }
        public int MinutesLeft { get; set; }
    }
}