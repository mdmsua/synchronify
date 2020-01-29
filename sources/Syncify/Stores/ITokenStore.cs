using System.Threading.Tasks;
using Syncify.Models;

namespace Syncify.Stores
{
    public interface ITokenStore
    {
        Task<Token?> GetTokenAsync(string id);

        Task SaveTokenAsync(Token token);
    }
}
