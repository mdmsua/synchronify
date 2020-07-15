using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class TrackBase
    {
        [JsonPropertyName("artists")]
        public IEnumerable<ArtistBase> Artists { get; set; } = Enumerable.Empty<ArtistBase>();

        [JsonPropertyName("available_markets")]
        public IEnumerable<string> AvailableMarkets { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("disc_number")]
        public int DiscNumber { get; set; }

        [JsonPropertyName("duration_ms")]
        public int Duration { get; set; }

        [JsonPropertyName("explicit")]
        public bool Explicit { get; set; }

        [JsonPropertyName("external_urls")]
        public IDictionary<string, Uri> ExternalUrls { get; set; } = new Dictionary<string, Uri>();

        [JsonPropertyName("href")]
        public Uri? Url { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("is_playable")]
        public bool IsPlayable { get; set; }

        [JsonPropertyName("linked_from")]
        public TrackLink? LinkedFrom { get; set; }

        [JsonPropertyName("restrictions")]
        public IDictionary<string, string>? Restrictions { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("preview_url")]
        public Uri PreviewUrl { get; set; }

        [JsonPropertyName("track_number")]
        public int TrackNumber { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;

        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }
    }
}
