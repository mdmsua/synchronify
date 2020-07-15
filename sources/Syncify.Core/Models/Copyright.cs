using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class Copyright
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
