using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Syncify.Models;
using Syncify.Providers;

namespace Syncify.Services
{
    public class HttpClientService : IHttpClientService
    {
        private static readonly Uri baseAddress = new Uri("https://api.spotify.com/");
        private const string authenticationScheme = "Bearer";

        private readonly MemoryCache memoryCache;
        private readonly IHttpClientFactory clientFactory;
        private readonly ITokenProvider tokenProvider;
        private readonly ILogger<HttpClientService> logger;
        private bool disposed;

        public HttpClientService(MemoryCache memoryCache, IHttpClientFactory clientFactory, ITokenProvider tokenProvider, ILogger<HttpClientService> logger)
        {
            this.memoryCache = memoryCache;
            this.clientFactory = clientFactory;
            this.tokenProvider = tokenProvider;
            this.logger = logger;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<HttpClient> GetHttpClientAsync(Token token)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (token.Id.Length == 0)
            {
                logger.LogInformation("Creating client for initial request");
                return CreateHttpClient(token.AccessToken);
            }

            using var scope = logger.BeginScope("Serving client for {id}", token.Id);
            return await memoryCache.GetOrCreateAsync(token.Id, async entry =>
            {
                logger.LogInformation("Cache exprired. Requesting token...");
                var refreshedToken = await tokenProvider.GetTokenAsync(token.Id);

                logger.LogInformation("Token requested. Creating client...");
                var client = CreateHttpClient(refreshedToken.AccessToken);

                return client;
            });
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                memoryCache?.Dispose();
                disposed = true;
            }
        }

        private HttpClient CreateHttpClient(string token)
        {
            var client = clientFactory.CreateClient();
            client.BaseAddress = baseAddress;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationScheme, token);
            return client;
        }
    }
}
