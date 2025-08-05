using InterviewService.Domain.Entities;
using InterviewService.Exceptions;
using InterviewService.Infrastructure.Data;
using InterviewService.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InterviewService.Application.Commands.StartInterview
{
    public class StartInterviewCommandHandler : IRequestHandler<StartInterviewCommand, Guid>
    {
        private readonly InterviewDbContext _context;
        private readonly IQuestionGeneratorService _questionGeneratorService;
        public StartInterviewCommandHandler(InterviewDbContext context, IQuestionGeneratorService questionGeneratorService)
        {
            _context = context;
            _questionGeneratorService = questionGeneratorService;
        }
        public async Task<Guid> Handle(StartInterviewCommand request, CancellationToken cancellationToken)
        {
            var interview = new Interview
            {
                Id = Guid.NewGuid(),
                CandidateId = request.CandidateId,
                Role = request.Role,
                StartedAt = DateTime.UtcNow,
                Questions = new List<Question>()
            };

            var generatedQuestions = await _questionGeneratorService.GenerateQuestionsAsync(request.Role);

            interview.Questions = generatedQuestions.Select(q => new Question
            {
                Id = Guid.NewGuid(),
                InterviewId = interview.Id,
                Text = q.Text,
                Source = q.Source
            }).ToList();

            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync(cancellationToken);

            return interview.Id;

        }
    }
}
