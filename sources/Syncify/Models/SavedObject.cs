using System;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class SavedObject
    {
        [JsonPropertyName("added_at")]
        [BsonElement("added_at")]
        public DateTime AddedAt { get; set; }
    }
}
