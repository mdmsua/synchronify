using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Syncify.Models;
using Syncify.Options;

namespace Syncify.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient client;
        private readonly SpotifyOptions options;
        private readonly IHttpContextAccessor contextAccessor;

        private bool disposed;

        const string scope = "user-read-private playlist-read-private user-library-read";

        public AuthorizationService(IOptions<SpotifyOptions> options, IHttpContextAccessor contextAccessor, HttpClient client)
        {
            if (options?.Value is null)
            {
                throw new ArgumentNullException(nameof(options.Value));
            }

            this.client = client;
            this.options = options.Value;
            this.contextAccessor = contextAccessor;
        }

        public Uri GetAuthorizationUri()
        {
            var state = contextAccessor.HttpContext.TraceIdentifier;
            var parameters = new Dictionary<string, string> { { "response_type", "code" }, { "client_id", options.ClientId }, { "scope", scope }, { "redirect_uri", options.RedirectUri.AbsoluteUri }, { "state", state } };
            var queryString = string.Join('&', parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            return new Uri($"https://accounts.spotify.com/authorize?{queryString}");
        }

        public async Task<Token> GetTokenAsync(string code)
        {
            using var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "code", code }, { "redirect_uri", options.RedirectUri.AbsoluteUri }, { "grant_type", "authorization_code" } });
            using var message = await client.PostAsync("/api/token", content).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<Token>(await message.Content.ReadAsStreamAsync().ConfigureAwait(false));
        }

        public async Task RefreshTokenAsync(Token token)
        {
            using var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "grant_type", "refresh_token" }, { "refresh_token", token.RefreshToken } });
            using var message = await client.PostAsync("/api/token", content).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();
            var (_, value, type, scope, expiration) = await JsonSerializer.DeserializeAsync<Token>(await message.Content.ReadAsStreamAsync().ConfigureAwait(false));
            token.AccessToken = value;
            token.Type = type;
            token.Scope = scope;
            token.Expiration = expiration;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                client?.Dispose();
                disposed = true;
            }
        }
    }
}
