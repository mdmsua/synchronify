using System.Threading.Tasks;

namespace Syncify.Handlers
{
    public interface ICronHandler
    {
        Task HandleTokensAsync();
    }
}
