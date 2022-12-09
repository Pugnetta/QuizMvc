using System.ComponentModel.DataAnnotations;

namespace AspNetMvcClass.Models.Domain
{
    public class CdTimer
    {
        [Key]
        public int Id { get; set; }
        public DateTime CurrentTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public int TotalDuration { get; set; }
        public int RemainingDuration { get; set; }
    }
}
