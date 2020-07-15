using Syncify.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Syncify.Core.Stores
{
    public interface IUserStore
    {
        IAsyncEnumerable<User> GetUsersAsync();

        Task<User?> GetUserAsync(string id);

        Task SaveUserAsync(User user);
    }
}
