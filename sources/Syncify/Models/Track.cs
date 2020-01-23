using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class Track : TrackBase
    {
        [JsonPropertyName("album")]
        [BsonElement("album")]
        public AlbumBase? Album { get; set; }

        [JsonPropertyName("external_ids")]
        [BsonElement("external_ids")]
        public IDictionary<string, string> ExternalIds { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("popularity")]
        [BsonElement("popularity")]
        public int Popularity { get; set; }
    }
}
