using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Syncify.Models
{
    public class Page<T>
    {
        [JsonPropertyName("href")]
        public Uri Uri { get; set; } = Defaults.Uri;

        [JsonPropertyName("items")]
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("next")]
        public Uri? Next { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("previous")]
        public Uri? Previous { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
