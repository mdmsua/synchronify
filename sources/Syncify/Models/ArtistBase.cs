using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class ArtistBase
    {
        [JsonPropertyName("external_urls")]
        [BsonElement("external_urls")]
        public IDictionary<string, Uri> ExternalUrls { get; set; } = new Dictionary<string, Uri>();

        [JsonPropertyName("href")]
        [BsonElement("href")]
        public Uri Url { get; set; } = Defaults.Uri;

        [JsonPropertyName("id")]
        [BsonId]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        [BsonElement("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        [BsonElement("uri")]
        public string Uri { get; set; } = string.Empty;
    }
}
