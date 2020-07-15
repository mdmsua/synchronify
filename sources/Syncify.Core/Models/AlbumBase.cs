using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class AlbumBase
    {
        [JsonPropertyName("album_group")]
        public virtual string? AlbumGroup { get; set; }

        [JsonPropertyName("album_type")]
        public string AlbumType { get; set; } = string.Empty;

        [JsonPropertyName("artists")]
        public IEnumerable<ArtistBase> Artists { get; set; } = Enumerable.Empty<ArtistBase>();

        [JsonPropertyName("available_markets")]
        public IEnumerable<string> AvailableMarkets { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("external_urls")]
        public IDictionary<string, Uri> ExternalUrls { get; set; } = new Dictionary<string, Uri>();

        [JsonPropertyName("href")]
        public Uri? Url { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("images")]
        public IEnumerable<Image> Images { get; set; } = Enumerable.Empty<Image>();

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;

        [JsonPropertyName("release_date_precision")]
        public string ReleaseDatePrecision { get; set; } = string.Empty;

        [JsonPropertyName("restrictions")]
        public IDictionary<string, string>? Restrictions { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;
    }
}
