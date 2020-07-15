using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Caching.Memory;

namespace Syncify.Core.Providers
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly CosmosClient client;
        private readonly IMemoryCache cache;

        public DatabaseProvider(CosmosClient client, IMemoryCache cache)
        {
            this.client = client;
            this.cache = cache;
        }

        public Task<Database> GetDatabaseAsync(string id) =>
            cache.GetOrCreateAsync<Database>(id, async _ => await client.CreateDatabaseIfNotExistsAsync(id));
    }
}
