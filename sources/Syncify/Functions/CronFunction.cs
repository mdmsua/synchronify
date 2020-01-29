using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Syncify.Handlers;

namespace Syncify
{
    public class CronFunction
    {
        private readonly ICronHandler handler;

        public CronFunction(ICronHandler handler)
        {
            this.handler = handler;
        }

        [FunctionName("Tokens")]
        public async Task Tokens([TimerTrigger("0 0 * * * *")] TimerInfo timer, ILogger log)
        {
            await handler.HandleTokensAsync().ConfigureAwait(false);
        }
    }
}
