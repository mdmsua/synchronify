using Syncify.Entities;
using Syncify.Models;
using System.Threading.Tasks;

namespace Syncify.Providers
{
    public interface ITokenProvider
    {
        Task<Token> GetTokenAsync(string id);

        Task<Token> GetTokenAsync(User user);
    }
}
