using System;

namespace Syncify.Core.Options
{
    public class SpotifyOptions
    {
        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;

        public Uri RedirectUri { get; set; }
    }
}
