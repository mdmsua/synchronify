using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class Episode : EpisodeBase
    {
        [JsonPropertyName("show")]
        public ShowBase Show { get; set; }
    }
}
