using System;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class Copyright
    {
        [JsonPropertyName("text")]
        [BsonElement("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        [BsonElement("type")]
        public string Type { get; set; } = string.Empty;
    }
}
