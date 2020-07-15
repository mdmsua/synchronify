using System.Threading.Tasks;
using Syncify.Core.Models;

namespace Syncify.Core.Stores
{
    public interface ITokenStore
    {
        Task<Token?> GetTokenAsync(string id);

        Task SaveTokenAsync(Token token);
    }
}
