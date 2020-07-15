using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class EpisodeBase
    {
        [JsonPropertyName("audio_preview_url")]
        public string? AudioPreviewUrl { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("duration_ms")]
        public int Duration { get; set; }

        [JsonPropertyName("explicit")]
        public bool Explicit { get; set; }

        [JsonPropertyName("external_urls")]
        public IDictionary<string, Uri> ExternalUrls { get; set; } = new Dictionary<string, Uri>();

        [JsonPropertyName("href")]
        public Uri? Url { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("images")]
        public IEnumerable<Image> Images { get; set; } = Enumerable.Empty<Image>();

        [JsonPropertyName("is_externally_hosted")]
        public bool IsExternallyHosted { get; set; }

        [JsonPropertyName("is_playable")]
        public bool IsPlayable { get; set; }

        [JsonPropertyName("languages")]
        public IEnumerable<string> Languages { get; set; } = Enumerable.Empty<string>();

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;

        [JsonPropertyName("release_date_precision")]
        public string ReleaseDatePrecision { get; set; } = string.Empty;

        [JsonPropertyName("resume_point")]
        public ResumePoint? ResumePoint { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        public string Uri { get; set; } = string.Empty;
    }
}
