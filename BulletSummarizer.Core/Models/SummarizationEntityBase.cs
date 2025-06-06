using System.Text.Json.Serialization;

namespace BulletSummarizer.Core.Models
{
    public abstract class SummarizationEntityBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("summarizationId")]
        public int SummarizationId { get; set; }
    }
}
