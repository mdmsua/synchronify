using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class Profile
    {
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("display_name")]
        [BsonElement("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("external_urls")]
        [BsonElement("external_urls")]
        public IDictionary<string, Uri> ExternalUrls { get; set; } = new Dictionary<string, Uri>();

        [JsonPropertyName("followers")]
        [BsonElement("followers")]
        public Followers Followers { get; set; } = new Followers();

        [JsonPropertyName("href")]
        [BsonElement("href")]
        public Uri Url { get; set; } = Defaults.Uri;

        [JsonPropertyName("id")]
        [BsonId]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("images")]
        [BsonElement("images")]
        public IEnumerable<Image> Images { get; set; } = Enumerable.Empty<Image>();

        [JsonPropertyName("product")]
        [BsonElement("product")]
        public string Product { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        [BsonElement("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        [BsonElement("uri")]
        public string Uri { get; set; } = string.Empty;
    }
}
