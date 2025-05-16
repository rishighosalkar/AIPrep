using MediatR;

namespace InterviewService.Commands.ScheduleInterview
{
    public record ScheduleInterviewCommand(string CandidateId, string Role, DateTime ScheduledAt) : IRequest<Guid>;
}
