using InterviewService.Domain.Entities.Nlp;

namespace InterviewService.Services
{
    public interface IQuestionGeneratorService
    {
        Task<List<GeneratedQuestionDto>> GenerateQuestionsAsync(string role);
    }
}
