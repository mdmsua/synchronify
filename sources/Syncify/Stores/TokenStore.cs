using System;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;
using Syncify.Models;

namespace Syncify.Stores
{
    public class TokenStore : ITokenStore
    {
        private readonly SecretClient client;

        public TokenStore(SecretClient client)
        {
            this.client = client;
        }

        public async Task<Token?> GetTokenAsync(string id)
        {
            var secret = await client.GetSecretAsync(id).ConfigureAwait(false);
            if (secret?.Value?.Properties?.ExpiresOn > DateTimeOffset.Now)
            {
                return new Token { Id = id, AccessToken = secret.Value.Value };
            }

            await client.StartDeleteSecretAsync(id).ConfigureAwait(false);

            return default;
        }

        public async Task SaveTokenAsync(Token token)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            var secret = new KeyVaultSecret(token.Id, token.AccessToken);
            secret.Properties.ExpiresOn = token.ExpiresOn;
            await client.SetSecretAsync(secret).ConfigureAwait(false);
        }
    }
}
