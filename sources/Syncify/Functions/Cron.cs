using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Syncify
{
    public class Cron
    {
        public Cron()
        {

        }

        [FunctionName("Refresh")]
        public static void Run([TimerTrigger("0 0 * * * *")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
