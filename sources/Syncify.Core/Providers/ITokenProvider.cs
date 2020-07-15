using Syncify.Core.Models;
using System.Threading.Tasks;

namespace Syncify.Core.Providers
{
    public interface ITokenProvider
    {
        Task<Token> GetTokenAsync(string id);

        Task<Token> GetTokenAsync(User user);
    }
}
