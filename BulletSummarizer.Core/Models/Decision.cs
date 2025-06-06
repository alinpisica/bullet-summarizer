using System.Text.Json.Serialization;

namespace BulletSummarizer.Core.Models
{
    public class Decision : SummarizationEntityBase
    {
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("decidedBy")]
        public string? DecidedBy { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }
    }
}
