using MediatR;

namespace InterviewService.Application.Commands.StartInterview
{
    public record StartInterviewCommand(Guid CandidateId, string Role) : IRequest<Guid>;
}
