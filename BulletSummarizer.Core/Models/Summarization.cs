using System.Text.Json.Serialization;

namespace BulletSummarizer.Core.Models
{
    public class Summarization
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("inboundEmailId")]
        public int InboundEmailId { get; set; }

        [JsonPropertyName("highLevelSummary")]
        public string? HighLevelSummary { get; set; }

        [JsonPropertyName("generalConversationalTone")]
        public string? GeneralConversationalTone { get; set; }

        [JsonPropertyName("participants")]
        public Participant[]? Participants { get; set; }

        [JsonPropertyName("actionItems")]
        public ActionItem[]? ActionItems { get; set; }

        [JsonPropertyName("decision")]
        public Decision[]? Decisions { get; set; }

        [JsonPropertyName("keyDatesAndDeadlines")]
        public KeyDate[]? KeyDatesAndDeadlines { get; set; }

        [JsonPropertyName("bullets")]
        public Bullet[]? Bullets { get; set; }
    }
}
