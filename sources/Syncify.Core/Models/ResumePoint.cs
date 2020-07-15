using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class ResumePoint
    {
        [JsonPropertyName("fully_played")]
        public bool FullyPlayed { get; set; }

        [JsonPropertyName("resume_position_ms")]
        public int ResumePosition { get; set; }
    }
}
