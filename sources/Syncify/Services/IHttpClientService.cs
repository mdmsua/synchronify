using System;
using System.Net.Http;
using System.Threading.Tasks;
using Syncify.Models;

namespace Syncify.Services
{
    public interface IHttpClientService : IDisposable
    {
        Task<HttpClient> GetHttpClientAsync(Token token);
    }
}
