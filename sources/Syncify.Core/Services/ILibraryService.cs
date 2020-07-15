using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syncify.Core.Models;

namespace Syncify.Core.Services
{
    public interface ILibraryService : IDisposable
    {
        Task<User> GetProfileAsync(Token token);

        Task<IReadOnlyList<Track>> GetTracksAsync(Token token);
    }
}
