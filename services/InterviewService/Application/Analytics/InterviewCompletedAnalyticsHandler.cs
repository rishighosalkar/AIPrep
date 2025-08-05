using InterviewService.Domain.Events;
using MediatR;

namespace InterviewService.Application.Analytics
{
    public class InterviewCompletedAnalyticsHandler : INotificationHandler<InterviewCompletedEvent>
    {
        private readonly ILogger<InterviewCompletedAnalyticsHandler> _logger;
        private readonly HttpClient _httpClient;
        public InterviewCompletedAnalyticsHandler(ILogger<InterviewCompletedAnalyticsHandler> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("AnayticsApi");
        }
        public async Task Handle(InterviewCompletedEvent notification, CancellationToken cancellationToken)
        {
            var payload = new
            {
                notification.InterviewId,
                notification.CandidateId,
                notification.Role,
                notification.FinishedAt
            };

            var response = await _httpClient.PostAsJsonAsync("/api/analytics/interview-finished", payload, cancellationToken);

            if(!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to send analytics for Interview {InterviewId}", notification.InterviewId);
            }
        }
    }
}
