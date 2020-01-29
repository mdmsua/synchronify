using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Syncify.Handlers
{
    public interface IHttpHandler
    {
        Task<IActionResult> HandleAuthorizeAsync();

        Task<IActionResult> HandleCallbackAsync(HttpRequest request);
    }
}
