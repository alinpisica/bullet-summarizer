namespace BulletSummarizer.Core.Models
{
    public class InboundEmail
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public int SummarizationId { get; set; }

        public int RetryCount { get; set; }

        public DateTimeOffset QueueEntryTime { get; set; }
    }
}
