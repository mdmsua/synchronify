using System;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class Followers
    {
        [JsonPropertyName("href")]
        public Uri? Link { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
