using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Syncify.Core.Providers;

namespace Syncify.Core.Services
{
    public interface IDatabaseService
    {
        Task BackupDatabaseAsync(string id);

        Task DropDatabaseAsync(string id);

        IDatabaseProvider Provider { get; }
    }
}
