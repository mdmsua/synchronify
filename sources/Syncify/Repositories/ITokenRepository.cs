using System.Collections.Generic;
using System.Threading.Tasks;
using Syncify.Models;

namespace Syncify.Repositories
{
    public interface ITokenRepository
    {
        Task<IEnumerable<Token>> GetTokensAsync();

        Task SaveTokenAsync(Token token);
    }
}