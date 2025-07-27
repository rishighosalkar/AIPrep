using InterviewService.Domain.Entities.Nlp;

namespace InterviewService.Services
{
    public class QuestionGeneratorService : IQuestionGeneratorService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<QuestionGeneratorService> _logger;

        public QuestionGeneratorService(HttpClient httpClient, ILogger<QuestionGeneratorService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<GeneratedQuestionDto>> GenerateQuestionsAsync(string role)
        {
            var request = new { role };
            var response = await _httpClient.PostAsJsonAsync("/api/questions/generate", request);

            if(!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get questions from NLP service. Status Code {0}", response.StatusCode);
                return new List<GeneratedQuestionDto>();
            }

            var questions = await response.Content.ReadFromJsonAsync<List<GeneratedQuestionDto>>();
            return questions ?? new List<GeneratedQuestionDto>();
        }
    }
}
