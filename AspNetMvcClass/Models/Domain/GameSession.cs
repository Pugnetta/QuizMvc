namespace AspNetMvcClass.Models.Domain
{
    public class GameSession
    {
        public int Id { get; set; }        
        public List<Domanda> Questions { get; set; }
        public int GameScore { get; set; }
    }
}
