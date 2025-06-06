using System.Text.Json.Serialization;

namespace BulletSummarizer.Core.DTOs
{
    public class InboundPostmarkPayload
    {
        [JsonPropertyName("From")]
        public string From { get; set; } = string.Empty;

        [JsonPropertyName("TextBody")]
        public string TextBody { get; set; } = string.Empty;

        [JsonPropertyName("HtmlBody")]
        public string HtmlBody { get; set; } = string.Empty;

        [JsonPropertyName("Date")]
        public string Date { get; set; } = string.Empty;
    }
}
