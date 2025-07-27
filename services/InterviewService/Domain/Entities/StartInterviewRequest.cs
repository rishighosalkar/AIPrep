namespace InterviewService.Domain.Entities
{
    public class StartInterviewRequest
    {
        public Guid CandidateId { get; set; }
        public string Role { get; set; }
    }
}
