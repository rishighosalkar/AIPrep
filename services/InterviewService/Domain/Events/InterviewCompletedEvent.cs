using MediatR;

namespace InterviewService.Domain.Events
{
    public class InterviewCompletedEvent: INotification
    {
        public Guid InterviewId { get; set; }
        public Guid CandidateId { get; set; }
        public string Role { get; }
        public DateTime FinishedAt { get; }

        public InterviewCompletedEvent(Guid interviewId, Guid candidateId, string role, DateTime finishedAt)
        {
            InterviewId = interviewId;
            CandidateId = candidateId;
            Role = role;
            FinishedAt = finishedAt;
        }
    }
}
