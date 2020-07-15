using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class Album : AlbumBase
    {
        [JsonIgnore]
        public override string? AlbumGroup => base.AlbumGroup;

        [JsonPropertyName("copyrights")]
        public IEnumerable<Copyright> Copyrights { get; set; } = Enumerable.Empty<Copyright>();

        [JsonPropertyName("external_ids")]
        public IDictionary<string, string> ExternalIds { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("genres")]
        public IEnumerable<string> Genres { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }

        [JsonPropertyName("tracks")]
        public Page<TrackBase>? Tracks { get; set; }
    }
}
