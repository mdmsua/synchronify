using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class SavedAlbum : SavedObject
    {
        [JsonPropertyName("album")]
        [BsonElement("album")]
        public Album? Album { get; set; }
    }
}
