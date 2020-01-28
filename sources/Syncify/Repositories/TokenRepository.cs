using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;
using Syncify.Models;

namespace Syncify.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly SecretClient client;

        public TokenRepository(SecretClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Token>> GetTokensAsync()
        {
            return await Task.FromResult(Enumerable.Empty<Token>());
        }

        public async Task SaveTokenAsync(Token token)
        {
            await Task.CompletedTask;
        }
    }
}
