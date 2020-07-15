using System;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class SavedObject
    {
        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }
    }
}
