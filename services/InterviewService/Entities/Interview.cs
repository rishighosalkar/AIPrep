namespace InterviewService.Entities
{
    public class Interview
    {
        public Guid Id { get; set; }
        public string CandidateId { get; set; }
        public string Role {  get; set; }
        public DateTime ScheduledAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
