using System;
using System.Threading.Tasks;
using Syncify.Models;

namespace Syncify.Services
{
    public interface IAuthorizationService : IDisposable
    {
        Uri GetAuthorizationUri();

        Task<Token> GetTokenAsync(string code);

        Task RefreshTokenAsync(Token token);
    }
}
