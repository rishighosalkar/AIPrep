using MediatR;

namespace InterviewService.Commands.FinishInterview
{
    public record FinishInterviewCommand(Guid InteriewId) : IRequest<Unit>;
}
