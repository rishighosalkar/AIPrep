using InterviewService.Domain.Entities;
using InterviewService.Infrastructure.Data;
using MediatR;

namespace InterviewService.Application.Commands.ScheduleInterview
{
    public class ScheduleInterviewCommandHandler : IRequestHandler<ScheduleInterviewCommand, Guid>
    {
        private readonly InterviewDbContext _context;
        public ScheduleInterviewCommandHandler(InterviewDbContext context)
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
