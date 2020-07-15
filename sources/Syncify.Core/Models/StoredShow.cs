using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class StoredShow : Show
    {
        [JsonPropertyName("episodes")]
        public new IEnumerable<EpisodeBase> Episodes { get; set; } = Enumerable.Empty<EpisodeBase>();
    }
}
