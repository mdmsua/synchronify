using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syncify.Models;

namespace Syncify.Services
{
    public interface ILibraryService : IDisposable
    {
        Task<Profile> GetProfileAsync(Token token);

        Task<IEnumerable<Track>> GetTracksAsync(Token token);
    }
}