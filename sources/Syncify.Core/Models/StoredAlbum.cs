using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class StoredAlbum : Album
    {
        [JsonPropertyName("tracks")]
        public new IEnumerable<TrackBase> Tracks { get; set; } = Enumerable.Empty<TrackBase>();
    }
}
