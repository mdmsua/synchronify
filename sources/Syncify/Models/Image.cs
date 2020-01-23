using System;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class Image
    {
        [JsonPropertyName("height")]
        [BsonElement("height")]
        public int? Height { get; set; }

        [JsonPropertyName("url")]
        [BsonElement("url")]
        public Uri Url { get; set; } = Defaults.Uri;

        [JsonPropertyName("width")]
        [BsonElement("width")]
        public int? Width { get; set; }
    }
}
