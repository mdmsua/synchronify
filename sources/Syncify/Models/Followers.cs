using System;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class Followers
    {
        [JsonPropertyName("href")]
        [BsonElement("href")]
        public Uri Link { get; set; } = Defaults.Uri;

        [JsonPropertyName("total")]
        [BsonElement("total")]
        public int Total { get; set; }
    }
}
