using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Syncify.Core.Models;

namespace Syncify.Core.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IHttpClientService clientService;
        private bool disposed;

        public LibraryService(IHttpClientService clientService)
        {
            this.clientService = clientService;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<User> GetProfileAsync(Token token)
        {
            using var client = await clientService.GetHttpClientAsync(token);
            using var message = await client.GetAsync("/v1/me");
            message.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<User>(await message.Content.ReadAsStreamAsync());
        }

        public async Task<IReadOnlyList<Track>> GetTracksAsync(Token token)
        {
            var tracks = new List<Track>();
            using var client = await clientService.GetHttpClientAsync(token);
            var uri = new Uri("/v1/me/tracks", UriKind.Relative);
            await foreach(var page in PageAsync<Track>(client, uri))
            {
                tracks.AddRange(page);
            }

            return tracks.AsReadOnly();
        }

        private async IAsyncEnumerable<IEnumerable<T>> PageAsync<T>(HttpClient client, Uri uri)
        {
            var done = false;
            do
            {
                using var message = await client.GetAsync(uri);
                message.EnsureSuccessStatusCode();
                var page = await JsonSerializer.DeserializeAsync<Page<T>>(await message.Content.ReadAsStreamAsync());
                yield return page.Items;
                if (page.Next is null)
                {
                    done = true;
                }
                else
                {
                    uri = page.Next!;
                }
            }
            while (!done);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                clientService?.Dispose();
                disposed = true;
            }
        }
    }
}
