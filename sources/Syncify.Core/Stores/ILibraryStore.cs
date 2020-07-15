using System.Collections.Generic;
using System.Threading.Tasks;
using Syncify.Core.Models;

namespace Syncify.Core.Stores
{
    public interface ILibraryStore
    {
        Task SaveTracksAsync(string id, IReadOnlyList<Track> tracks);

        Task SaveAlbumsAsync(string id, IReadOnlyList<StoredAlbum> albums);

        Task SaveShowsAsync(string id, IReadOnlyList<StoredShow> shows);
    }
}
