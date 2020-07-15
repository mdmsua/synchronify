using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class User
    {
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("external_urls")]
        public IDictionary<string, Uri> ExternalUrls { get; set; } = new Dictionary<string, Uri>();

        [JsonPropertyName("followers")]
        public Followers Followers { get; set; } = new Followers();

        [JsonPropertyName("href")]
        public Uri? Url { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("images")]
        public IEnumerable<Image> Images { get; set; } = Enumerable.Empty<Image>();

        [JsonPropertyName("product")]
        public string Product { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;
    }
}
