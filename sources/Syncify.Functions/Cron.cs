using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Syncify.Functions
{
    public static class Cron
    {
        [FunctionName(nameof(Run))]
        public static async Task Run([TimerTrigger("0 0 * * * *")] TimerInfo timer, [DurableClient] IDurableOrchestrationClient client, ILogger log)
        {
            var instance = await client.StartNewAsync(nameof(Orchestrators.Main)).ConfigureAwait(false);
            log.LogInformation("Orchestrator {id} started", instance);
        }
    }
}
