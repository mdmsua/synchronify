using System;
using System.Text.Json.Serialization;

namespace Syncify.Core.Models
{
    public class Token
    {
        [JsonIgnore]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("scope")]
        public string Scope { get; set; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public int Expiration { get; set; }

        public void Deconstruct(out string id, out string value, out string type, out string scope, out int expiration)
        {
            id = Id;
            value = AccessToken;
            type = Type;
            scope = Scope;
            expiration = Expiration;
        }
    }
}
