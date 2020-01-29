using Microsoft.Extensions.Logging;
using Syncify.Providers;
using Syncify.Stores;
using System.Globalization;
using System.Threading.Tasks;

namespace Syncify.Handlers
{
    public class CronHandler : ICronHandler
    {
        private readonly IUserStore userStore;
        private readonly ITokenProvider tokenProvider;
        private readonly ILogger<CronHandler> logger;

        public CronHandler(IUserStore userStore, ITokenProvider tokenProvider, ILogger<CronHandler> logger)
        {
            this.userStore = userStore;
            this.tokenProvider = tokenProvider;
            this.logger = logger;
        }

        public async Task HandleTokensAsync()
        {
            await foreach (var user in userStore.GetUsersAsync())
            {
                var id = user.RowKey;
                logger.LogInformation("Processing token for user {id}...", id);
                var token = await tokenProvider.GetTokenAsync(user).ConfigureAwait(false);
                logger.LogInformation("Done. Token expires on {expiration}", token.ExpiresOn.ToString("F", CultureInfo.InvariantCulture));
            }
        }
    }
}
