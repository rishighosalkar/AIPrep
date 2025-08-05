using MediatR;

namespace InterviewService.Application.Commands.ScheduleInterview
{
    public record ScheduleInterviewCommand(Guid CandidateId, string Role, DateTime ScheduledAt) : IRequest<Guid>;
}
