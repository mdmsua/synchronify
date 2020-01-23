using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class AlbumBase
    {
        [JsonPropertyName("album_group")]
        [BsonElement("album_group")]
        public virtual string? AlbumGroup { get; set; }

        [JsonPropertyName("album_type")]
        [BsonElement("album_type")]
        public string AlbumType { get; set; } = string.Empty;

        [JsonPropertyName("artists")]
        [BsonElement("artists")]
        public IEnumerable<ArtistBase> Artists { get; set; } = Enumerable.Empty<ArtistBase>();

        [JsonPropertyName("available_markets")]
        [BsonElement("available_markets")]
        public IEnumerable<string> AvailableMarkets { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("external_urls")]
        [BsonElement("external_urls")]
        public IDictionary<string, Uri> ExternalUrls { get; set; } = new Dictionary<string, Uri>();

        [JsonPropertyName("href")]
        [BsonElement("href")]
        public Uri Url { get; set; } = Defaults.Uri;

        [JsonPropertyName("id")]
        [BsonId]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("images")]
        [BsonElement("images")]
        public IEnumerable<Image> Images { get; set; } = Enumerable.Empty<Image>();

        [JsonPropertyName("name")]
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("release_date")]
        [BsonElement("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;

        [JsonPropertyName("release_date_precision")]
        [BsonElement("release_date_precision")]
        public string ReleaseDatePrecision { get; set; } = string.Empty;

        [JsonPropertyName("restrictions")]
        [BsonElement("restrictions")]
        public IDictionary<string, string>? Restrictions { get; set; }

        [JsonPropertyName("type")]
        [BsonElement("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        [BsonElement("uri")]
        public string Uri { get; set; } = string.Empty;
    }
}
