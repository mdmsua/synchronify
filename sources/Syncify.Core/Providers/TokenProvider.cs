using Microsoft.Extensions.Logging;
using Syncify.Core.Models;
using Syncify.Core.Services;
using Syncify.Core.Stores;
using System;
using System.Threading.Tasks;

namespace Syncify.Core.Providers
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ITokenStore tokenStore;
        private readonly IUserStore userStore;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<TokenProvider> logger;

        public TokenProvider(ITokenStore tokenStore, IUserStore userStore, IAuthorizationService authorizationService, ILogger<TokenProvider> logger)
        {
            this.tokenStore = tokenStore;
            this.authorizationService = authorizationService;
            this.userStore = userStore;
            this.logger = logger;
        }

        public async Task<Token> GetTokenAsync(string id)
        {
            logger.LogInformation("Getting token for user {id}...", id);
            var token = await tokenStore.GetTokenAsync(id).ConfigureAwait(false);
            if (token is null)
            {
                var user = await userStore.GetUserAsync(id).ConfigureAwait(false);
                if (user is null)
                {
                    // TODO throw new UserNotFoundException { Id = id };
                    throw new Exception();
                }

                return await GetTokenAsync(user).ConfigureAwait(false);
            }

            return token;
        }

        public async Task<Token> GetTokenAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var token = await tokenStore.GetTokenAsync(user.Id).ConfigureAwait(false);
            if (token is null)
            {
                logger.LogInformation("Token not found or expired. Requesting new one...");
                token = new Token { Id = user.Id };
                await authorizationService.RefreshTokenAsync(token).ConfigureAwait(false);
                logger.LogInformation("Saving token...");
                await tokenStore.SaveTokenAsync(token).ConfigureAwait(false);
                logger.LogInformation("Done");
            }

            return token;
        }
    }
}
