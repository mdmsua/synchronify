using Microsoft.Azure.Cosmos;
using Syncify.Core.Providers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using User = Syncify.Core.Models.User;

namespace Syncify.Core.Stores
{
    public class UserStore : IUserStore
    {
        private readonly IDatabaseProvider provider;

        public UserStore(IDatabaseProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public async Task<User?> GetUserAsync(string id)
        {
            var container = await GetContainerAsync().ConfigureAwait(false);
            try
            {
                var user = await container.ReadItemAsync<User>(id, PartitionKey.None).ConfigureAwait(false);
                return user.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }
        }

        public async IAsyncEnumerable<User> GetUsersAsync()
        {
            string? token = default;
            var container = await GetContainerAsync().ConfigureAwait(false);
            do
            {
                var iterator = container.GetItemQueryIterator<User>(continuationToken: token);
                var response = await iterator.ReadNextAsync().ConfigureAwait(false);
                token = iterator.HasMoreResults ? response.ContinuationToken : default;
                foreach (var user in response.Resource)
                {
                    yield return user;
                }
            } while (token != default);
        }

        public async Task SaveUserAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var container = await GetContainerAsync().ConfigureAwait(false);
            await container.CreateItemAsync(user).ConfigureAwait(false);
        }

        private async Task<Container> GetContainerAsync()
        {
            var database = await provider.GetDatabaseAsync("spotify").ConfigureAwait(false);
            return await database.CreateContainerIfNotExistsAsync("users", $"/{nameof(User.Type).ToLowerInvariant()}").ConfigureAwait(false);
        }
    }
}
