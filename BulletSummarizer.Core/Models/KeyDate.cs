using System.Text.Json.Serialization;

namespace BulletSummarizer.Core.Models
{
    public class KeyDate : SummarizationEntityBase
    {
        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }
    }
}
