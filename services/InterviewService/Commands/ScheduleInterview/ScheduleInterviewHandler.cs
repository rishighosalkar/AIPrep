using InterviewService.Data;
using InterviewService.Entities;
using MediatR;

namespace InterviewService.Commands.ScheduleInterview
{
    public class ScheduleInterviewHandler : IRequestHandler<ScheduleInterviewCommand, Guid>
    {
        private readonly InterviewDbContext _context;
        public ScheduleInterviewHandler(InterviewDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(ScheduleInterviewCommand request, CancellationToken cancellationToken)
        {
            var interview = new Interview
            {
                CandidateId = request.CandidateId,
                Role = request.Role,
                ScheduledAt = request.ScheduledAt
            };

            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync(cancellationToken);

            return interview.Id;
        }
    }
}
