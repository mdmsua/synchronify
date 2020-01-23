using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Syncify.Models
{
    public class Artist : ArtistBase
    {
        [JsonPropertyName("followers")]
        [BsonElement("followers")]
        public Followers Followers { get; set; } = new Followers();

        [JsonPropertyName("genres")]
        [BsonElement("genres")]
        public IEnumerable<string> Genres { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("images")]
        [BsonElement("images")]
        public IEnumerable<Image> Images { get; set; } = Enumerable.Empty<Image>();

        [JsonPropertyName("popularity")]
        [BsonElement("popularity")]
        public int Popularity { get; set; }
    }
}
