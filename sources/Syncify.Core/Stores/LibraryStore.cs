using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Syncify.Core.Models;
using Syncify.Core.Providers;

namespace Syncify.Core.Stores
{
    public class LibraryStore : ILibraryStore
    {
        private readonly IDatabaseProvider provider;

        public LibraryStore(IDatabaseProvider provider)
{
            this.provider = provider;
        }

        public async Task SaveAlbumsAsync(string id, IReadOnlyList<StoredAlbum> albums)
        {
            var database = await provider.GetDatabaseAsync(id);
            Container container = await database.CreateContainerIfNotExistsAsync("albums", nameof(Album.Id).ToLowerInvariant());
            await CreateItemsAsync(albums, container);
        }

        public async Task SaveShowsAsync(string id, IReadOnlyList<StoredShow> shows)
        {
            var database = await provider.GetDatabaseAsync(id);
            Container container = await database.CreateContainerIfNotExistsAsync("shows", nameof(Show.Id).ToLowerInvariant());
            await CreateItemsAsync(shows, container);
        }

        public async Task SaveTracksAsync(string id, IReadOnlyList<Track> tracks)
        {
            var database = await provider.GetDatabaseAsync(id);
            Container container = await database.CreateContainerIfNotExistsAsync("tracks", nameof(Track.Id).ToLowerInvariant());
            await CreateItemsAsync(tracks, container);
        }

        private static Task CreateItemsAsync<T>(IReadOnlyList<T> items, Container container) =>
            Task.WhenAll(items.Select(item => container.CreateItemAsync(item)));
    }
}
