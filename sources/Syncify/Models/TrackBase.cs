using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class TrackBase
    {
        [JsonPropertyName("artists")]
        [BsonElement("artists")]
        public IEnumerable<ArtistBase> Artists { get; set; } = Enumerable.Empty<ArtistBase>();

        [JsonPropertyName("available_markets")]
        [BsonElement("available_markets")]
        public IEnumerable<string> AvailableMarkets { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("disc_number")]
        [BsonElement("disc_number")]
        public int DiscNumber { get; set; }

        [JsonPropertyName("duration_ms")]
        [BsonElement("duration_ms")]
        public int Duration { get; set; }

        [JsonPropertyName("explicit")]
        [BsonElement("explicit")]
        public bool Explicit { get; set; }

        [JsonPropertyName("external_urls")]
        [BsonElement("external_urls")]
        public IDictionary<string, Uri> ExternalUrls { get; set; } = new Dictionary<string, Uri>();

        [JsonPropertyName("href")]
        [BsonElement("href")]
        public Uri Url { get; set; } = Defaults.Uri;

        [JsonPropertyName("id")]
        [BsonId]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("is_playable")]
        [BsonElement("is_playable")]
        public bool IsPlayable { get; set; }

        [JsonPropertyName("linked_from")]
        [BsonElement("linked_from")]
        public TrackLink? LinkedFrom { get; set; }

        [JsonPropertyName("restrictions")]
        [BsonElement("restrictions")]
        public IDictionary<string, string>? Restrictions { get; set; }

        [JsonPropertyName("name")]
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("preview_url")]
        [BsonElement("preview_url")]
        public Uri PreviewUrl { get; set; } = Defaults.Uri;

        [JsonPropertyName("track_number")]
        [BsonElement("track_number")]
        public int TrackNumber { get; set; }

        [JsonPropertyName("type")]
        [BsonElement("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        [BsonElement("uri")]
        public string Uri { get; set; } = string.Empty;

        [JsonPropertyName("is_local")]
        [BsonElement("is_local")]
        public bool IsLocal { get; set; }
    }
}
