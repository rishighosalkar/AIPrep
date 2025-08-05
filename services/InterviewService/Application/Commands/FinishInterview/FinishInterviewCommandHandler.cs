using InterviewService.Domain.Events;
using InterviewService.Exceptions;
using InterviewService.Infrastructure.Data;
using MediatR;

namespace InterviewService.Application.Commands.FinishInterview
{
    public class FinishInterviewCommandHandler : IRequestHandler<FinishInterviewCommand, Unit>
    {
        private readonly InterviewDbContext _context;
        private readonly IMediator _mediator;
        public FinishInterviewCommandHandler(InterviewDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(FinishInterviewCommand request, CancellationToken cancellationToken)
        {
            var interview = await _context.Interviews.FindAsync(request.InteriewId);

            if (interview is null)
                throw new NotFoundException("Interview not found.");

            interview.FinishedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var @event = new InterviewCompletedEvent(interview.Id, interview.CandidateId, interview.Role, interview.FinishedAt.Value);
            
            await _mediator.Publish(@event, cancellationToken);

            return Unit.Value;
        }
    }
}
