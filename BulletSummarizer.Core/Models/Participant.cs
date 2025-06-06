using System.Text.Json.Serialization;

namespace BulletSummarizer.Core.Models
{
    public class Participant : SummarizationEntityBase
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }
}
