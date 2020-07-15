using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.AspNetCore.Mvc;

namespace Syncify.Functions
{
    public static class Http
    {
        [FunctionName(nameof(Http))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, [DurableClient] IDurableOrchestrationClient client, ILogger log)
        {
            var instance = await client.StartNewAsync(nameof(Orchestrators.Main)).ConfigureAwait(false);
            log.LogInformation("Orchestrator {id} started", instance);
            return client.CreateCheckStatusResponse(req, instance);
        }
    }
}
