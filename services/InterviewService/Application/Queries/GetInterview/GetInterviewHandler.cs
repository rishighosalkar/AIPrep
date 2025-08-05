using InterviewService.Exceptions;
using InterviewService.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InterviewService.Application.Queries.GetInterview
{
    public class GetInterviewHandler : IRequestHandler<GetInterviewQuery, InterviewDto>
    {
        private readonly InterviewDbContext _context;
        public GetInterviewHandler(InterviewDbContext context)
        {
            _context = context;
        }
        public async Task<InterviewDto> Handle(GetInterviewQuery request, CancellationToken cancellationToken)
        {
            var interview = await _context.Interviews.Include(i => i.Questions)
                                    .FirstOrDefaultAsync(i => i.Id == request.InterviewId, cancellationToken);

            if (interview is null)
                throw new NotFoundException("Interview not found.");

            return new InterviewDto {
                Id = interview.Id,
                CandidateId = interview.CandidateId,
                Role = interview.Role,
                Questions = interview.Questions.Select(q => new QuestionDto
                {
                    Text = q.Text,
                    Answer = q.Answer
                }).ToList()
            };
        }
    }
}
