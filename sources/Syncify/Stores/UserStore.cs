using Microsoft.Azure.Cosmos.Table;
using Syncify.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Syncify.Stores
{
    public class UserStore : IUserStore
    {
        private readonly CloudTable table;

        public UserStore(CloudTableClient client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            table = client.GetTableReference("users");
            table.CreateIfNotExists();
        }

        public async Task<User?> GetUserAsync(string id)
        {
            var operation = TableOperation.Retrieve<User>("user", id);
            var result = await table.ExecuteAsync(operation).ConfigureAwait(false);
            return result?.Result as User;
        }

        public async IAsyncEnumerable<User> GetUsersAsync()
        {
            TableContinuationToken? token = default;
            var query = new TableQuery<User>();
            do
            {
                var result = await table.ExecuteQuerySegmentedAsync(query, token).ConfigureAwait(false);
                token = result.ContinuationToken;
                foreach (var user in result.Results)
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

            var operation = TableOperation.Retrieve<User>(user.PartitionKey, user.RowKey);
            var result = await table.ExecuteAsync(operation).ConfigureAwait(false);
            if (result.Result is null)
            {
                operation = TableOperation.Insert(user);
            }
            else
            {
                operation = TableOperation.Merge(user);
            }

            await table.ExecuteAsync(operation).ConfigureAwait(false);
        }
    }
}
