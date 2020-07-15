using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Syncify.Core.Providers
{
    public interface IDatabaseProvider
    {
        Task<Database> GetDatabaseAsync(string id);
    }
}
