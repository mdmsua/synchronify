using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class TrackLink
    {
        [JsonPropertyName("external_urls")]
        public IDictionary<string, Uri> ExternalUrls { get; set; } = new Dictionary<string, Uri>();

        [JsonPropertyName("href")]
        public Uri? Url { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;
    }
}
