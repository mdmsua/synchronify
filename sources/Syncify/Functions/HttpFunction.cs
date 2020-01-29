using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Syncify.Handlers;

namespace Syncify.Functions
{
    public class HttpFunction
    {
        private readonly IHttpHandler handler;

        public HttpFunction(IHttpHandler handler)
        {
            this.handler = handler;
        }

        [FunctionName("Authorize")]
        public Task<IActionResult> Authorize(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "auth")] HttpRequest req)
        {
            return handler.HandleAuthorizeAsync();
        }

        [FunctionName("Callback")]
        public Task<IActionResult> Callback(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "auth/callback")] HttpRequest req,
            ILogger log)
        {
            return handler.HandleCallbackAsync(req);
        }
    }
}
