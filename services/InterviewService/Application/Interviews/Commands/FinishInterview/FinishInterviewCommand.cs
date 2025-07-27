using MediatR;

namespace InterviewService.Application.Interviews.Commands.FinishInterview
{
    public record FinishInterviewCommand(Guid InteriewId) : IRequest<Unit>;
}
