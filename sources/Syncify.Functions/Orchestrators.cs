using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Syncify.Core.Models;

namespace Syncify.Functions
{
    public static class Orchestrators
    {
        [FunctionName(nameof(Main))]
        public static async Task Main([OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var users = await context.CallActivityAsync<User[]>(nameof(Activities.Users), default).ConfigureAwait(false);

            // var tasks = new List<Task>(from user in users select context.CallSubOrchestratorAsync(nameof(Orchestrators.Sync), user));

            // await Task.WhenAll(tasks).ConfigureAwait(false);

            await context.CallSubOrchestratorAsync(nameof(Sync), users.Single()).ConfigureAwait(false);
        }

        [FunctionName(nameof(Sync))]
        public static async Task Sync([OrchestrationTrigger] IDurableOrchestrationContext context, ILogger logger)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var user = context.GetInput<User>();
            var id = user.Id;

            logger.LogInformation(id);

            await context.CallActivityAsync(nameof(Activities.Backup), id).ConfigureAwait(false);
            await context.CallActivityAsync(nameof(Activities.Drop), id).ConfigureAwait(false);

            var token = await context.CallActivityAsync<Token>(nameof(Activities.Token), id).ConfigureAwait(false);

            logger.LogInformation(token.AccessToken);

            var trackTask = context.CallActivityAsync<int>(nameof(Activities.Tracks), token);
            var albumTask = context.CallActivityAsync<int>(nameof(Activities.Albums), token);
            var playlistTask = context.CallActivityAsync<int>(nameof(Activities.Playlists), token);

            var started = context.CurrentUtcDateTime;
            await Task.WhenAll(trackTask, albumTask, playlistTask).ConfigureAwait(false);
            var finished = context.CurrentUtcDateTime;

            await context.CallActivityAsync(nameof(Activities.Log), (id, started, finished, await trackTask.ConfigureAwait(false), await albumTask.ConfigureAwait(false), await playlistTask.ConfigureAwait(false))).ConfigureAwait(false);
        }
    }
}
