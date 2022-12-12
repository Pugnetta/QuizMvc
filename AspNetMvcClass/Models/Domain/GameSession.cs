namespace AspNetMvcClass.Models.Domain
{
    public class GameSession
    {
        
        public int Id { get; set; }
        public string? UserId { get; set; }
        public List<Domanda> Questions { get; set; } = null!;
        public int CurrentQuestionIndex { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
