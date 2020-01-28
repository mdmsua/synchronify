using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Syncify.Models;

namespace Syncify.Services
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

        public async Task<Profile> GetProfileAsync(Token token)
        {
            using var client = await clientService.GetHttpClientAsync(token);
            using var message = await client.GetAsync("/v1/me");
            message.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<Profile>(await message.Content.ReadAsStreamAsync());
        }

        public Task<IEnumerable<Track>> GetTracksAsync(Token token)
        {
            throw new NotImplementedException();
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
