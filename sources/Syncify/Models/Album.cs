using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class Album : AlbumBase
    {
        [JsonIgnore]
        [BsonIgnore]
        public override string? AlbumGroup => base.AlbumGroup;

        [JsonPropertyName("copyrights")]
        [BsonElement("copyrights")]
        public IEnumerable<Copyright> Copyrights { get; set; } = Enumerable.Empty<Copyright>();

        [JsonPropertyName("external_ids")]
        [BsonElement("external_ids")]
        public IDictionary<string, string> ExternalIds { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("genres")]
        [BsonElement("genres")]
        public IEnumerable<string> Genres { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("label")]
        [BsonElement("label")]
        public string Label { get; set; } = string.Empty;

        [JsonPropertyName("popularity")]
        [BsonElement("popularity")]
        public int Popularity { get; set; }

        [JsonPropertyName("tracks")]
        [BsonElement("tracks")]
        public Page<TrackBase>? Tracks { get; set; }
    }
}
