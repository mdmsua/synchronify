using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class Track : TrackBase
    {
        [JsonPropertyName("album")]
        public AlbumBase? Album { get; set; }

        [JsonPropertyName("external_ids")]
        public IDictionary<string, string> ExternalIds { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }
    }
}
