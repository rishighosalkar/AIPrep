using InterviewService.Domain.Entities;
using MediatR;

namespace InterviewService.Application.Queries.GetInterview
{
    public record GetInterviewQuery(Guid InterviewId) : IRequest<InterviewDto>;

    public class InterviewDto
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string Role {  get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }

    public class QuestionDto
    {
        public string Text { get; set; } = string.Empty;
        public string? Answer { get; set; }
    }
}
