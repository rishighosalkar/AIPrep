using MediatR;

namespace InterviewService.Application.Commands.FinishInterview
{
    public record FinishInterviewCommand(Guid InteriewId) : IRequest<Unit>;
}
