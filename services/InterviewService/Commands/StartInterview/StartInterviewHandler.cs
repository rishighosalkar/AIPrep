using InterviewService.Data;
using InterviewService.Exceptions;
using MediatR;

namespace InterviewService.Commands.StartInterview
{
    public class StartInterviewHandler : IRequestHandler<StartInterviewCommand, Unit>
    {
        private readonly InterviewDbContext _context;
        public StartInterviewHandler(InterviewDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(StartInterviewCommand request, CancellationToken cancellationToken)
        {
            var interview = await _context.Interviews.FindAsync(request.InterviewId);

            if(interview is null)
                throw new NotFoundException("");

            interview.StartedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
