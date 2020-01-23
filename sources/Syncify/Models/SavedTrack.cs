using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class SavedTrack : SavedObject
    {
        [JsonPropertyName("track")]
        [BsonElement("track")]
        public Track? Track { get; set; }
    }
}
