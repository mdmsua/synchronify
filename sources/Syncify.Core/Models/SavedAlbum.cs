using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class SavedAlbum : SavedObject
    {
        [JsonPropertyName("album")]
        public Album? Album { get; set; }
    }
}
