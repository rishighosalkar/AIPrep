using MediatR;

namespace InterviewService.Commands.StartInterview
{
    public record StartInterviewCommand(Guid InterviewId) : IRequest<Unit>;
}
