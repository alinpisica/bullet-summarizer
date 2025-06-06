using System.Text.Json.Serialization;

namespace BulletSummarizer.Core.Models
{
    public class Bullet : SummarizationEntityBase
    {
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("relatedTo")]
        public string? RelatedTo { get; set; }
    }
}
