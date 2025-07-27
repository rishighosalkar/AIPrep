using MediatR;

namespace InterviewService.Application.Interviews.Commands.ScheduleInterview
{
    public record ScheduleInterviewCommand(Guid CandidateId, string Role, DateTime ScheduledAt) : IRequest<Guid>;
}
