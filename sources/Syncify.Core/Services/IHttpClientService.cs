using System;
using System.Net.Http;
using System.Threading.Tasks;
using Syncify.Core.Models;

namespace Syncify.Core.Services
{
    public interface IHttpClientService : IDisposable
    {
        Task<HttpClient> GetHttpClientAsync(Token token);
    }
}
