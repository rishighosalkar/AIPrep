using InterviewService.Data;
using InterviewService.Exceptions;
using MediatR;

namespace InterviewService.Application.Interviews.Commands.FinishInterview
{
    public class FinishInterviewHandler : IRequestHandler<FinishInterviewCommand, Unit>
    {
        private readonly InterviewDbContext _context;
        public FinishInterviewHandler(InterviewDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(FinishInterviewCommand request, CancellationToken cancellationToken)
        {
            var interview = await _context.Interviews.FindAsync(request.InteriewId);

            if (interview is null)
                throw new NotFoundException("Interview not found.");

            interview.FinishedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
