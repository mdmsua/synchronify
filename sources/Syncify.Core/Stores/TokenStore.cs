using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;
using Syncify.Core.Models;

namespace Syncify.Core.Stores
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
            var value = secret?.Value?.Value;
            var token = await DeserializeTokenAsync(value);
            if (token != default)
            {
                token.Id = id;
            }
            return token;
        }

        public async Task SaveTokenAsync(Token token)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            var value = await SerializeTokenAsync(token);
            var secret = new KeyVaultSecret(token.Id, value);
            await client.SetSecretAsync(secret).ConfigureAwait(false);
        }

        private static async Task<string> SerializeTokenAsync(Token token)
        {
            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, token);
            stream.Seek(0L, SeekOrigin.Begin);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        private static async Task<Token?> DeserializeTokenAsync(string? value)
        {
            if (value is null)
            {
                return default;
            }

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(value));
            try
            {
                return await JsonSerializer.DeserializeAsync<Token>(stream);
            }
            catch
            {
                return default;
            }
        }
    }
}
