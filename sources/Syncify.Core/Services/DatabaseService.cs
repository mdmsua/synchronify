using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Syncify.Core.Providers;

namespace Syncify.Core.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> logger;

        public DatabaseService(IDatabaseProvider provider, ILogger<DatabaseService> logger)
        {
            Provider = provider;
            this.logger = logger;
        }

        public async Task BackupDatabaseAsync(string id)
        {
            using var scope = logger.BeginScope("Backing up database {id}...", id);
            await Provider.GetDatabaseAsync(id);
            logger.LogInformation("Done");
        }

        public async Task DropDatabaseAsync(string id)
        {
            using var scope = logger.BeginScope("Dropping database {id}...", id);
            var database = await Provider.GetDatabaseAsync(id);
            await database.DeleteAsync();
            logger.LogInformation("Done");
        }

        public IDatabaseProvider Provider { get; }
    }
}
