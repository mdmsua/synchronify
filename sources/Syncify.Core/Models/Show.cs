using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class Show : ShowBase
    {
        [JsonPropertyName("episodes")]
        public Page<EpisodeBase>? Episodes { get; set; }
    }
}
