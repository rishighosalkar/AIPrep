namespace InterviewService.Entities
{
    public class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public string? Answer { get; set; }
        public string Source { get; set; } = "AI";

        public Guid InterviewId { get; set; }
        public Interview Interview { get; set; } = null;

    }
}
