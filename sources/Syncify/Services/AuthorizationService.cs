using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
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
            var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "code", code }, { "redirect_uri", options.RedirectUri.AbsoluteUri }, { "grant_type", "authorization_code" } });
            var message = await client.PostAsync("/api/token", content);
            message.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<Token>(await message.Content.ReadAsStreamAsync());
        }

        public async Task RefreshTokenAsync(Token token)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "grant_type", "refresh_token" }, { "refresh_token", token.RefreshToken } });
            var message = await client.PostAsync("/api/token", content);
            message.EnsureSuccessStatusCode();
            var (_, value, type, scope, expiration) = await JsonSerializer.DeserializeAsync<Token>(await message.Content.ReadAsStreamAsync());
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
