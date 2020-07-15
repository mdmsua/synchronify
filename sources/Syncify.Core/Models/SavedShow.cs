using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class SavedShow : SavedObject
    {
        [JsonPropertyName("show")]
        public Show Show { get; set; }
    }
}
