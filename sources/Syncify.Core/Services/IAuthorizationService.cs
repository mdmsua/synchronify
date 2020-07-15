using System;
using System.Threading.Tasks;
using Syncify.Core.Models;

namespace Syncify.Core.Services
{
    public interface IAuthorizationService : IDisposable
    {
        Uri GetAuthorizationUri();

        Task<Token> GetTokenAsync(string code);

        Task RefreshTokenAsync(Token token);
    }
}
