using System;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class Image
    {
        [JsonPropertyName("height")]
        public int? Height { get; set; }

        [JsonPropertyName("url")]
        public Uri? Url { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }
    }
}
