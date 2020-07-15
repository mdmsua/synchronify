using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class Artist : ArtistBase
    {
        [JsonPropertyName("followers")]
        public Followers Followers { get; set; } = new Followers();

        [JsonPropertyName("genres")]
        public IEnumerable<string> Genres { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("images")]
        public IEnumerable<Image> Images { get; set; } = Enumerable.Empty<Image>();

        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }
    }
}
