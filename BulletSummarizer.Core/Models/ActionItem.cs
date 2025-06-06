using System.Text.Json.Serialization;

namespace BulletSummarizer.Core.Models
{
    public class ActionItem : SummarizationEntityBase
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("assignee")]
        public string? Assignee { get; set; }

        [JsonPropertyName("dueDate")]
        public string? DueDate { get; set; }
    }
}
