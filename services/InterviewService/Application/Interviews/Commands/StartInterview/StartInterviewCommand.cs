using MediatR;

namespace InterviewService.Application.Interviews.Commands.StartInterview
{
    public record StartInterviewCommand(Guid CandidateId, string Role) : IRequest<Guid>;
}
