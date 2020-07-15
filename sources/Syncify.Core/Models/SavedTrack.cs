using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class SavedTrack : SavedObject
    {
        [JsonPropertyName("track")]
        public Track? Track { get; set; }
    }
}
