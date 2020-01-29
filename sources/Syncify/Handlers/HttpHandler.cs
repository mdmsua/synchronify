using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syncify.Entities;
using Syncify.Services;
using Syncify.Stores;
using System;
using System.Threading.Tasks;

namespace Syncify.Handlers
{
    public class HttpHandler : IHttpHandler
    {
        private readonly IAuthorizationService authorizationService;
        private readonly ILibraryService libraryService;
        private readonly ITokenStore tokenStore;
        private readonly IUserStore userStore;

        public HttpHandler(IAuthorizationService authorizationService, ILibraryService libraryService, ITokenStore tokenStore, IUserStore userStore)
        {
            this.authorizationService = authorizationService;
            this.libraryService = libraryService;
            this.tokenStore = tokenStore;
            this.userStore = userStore;
        }

        public Task<IActionResult> HandleAuthorizeAsync()
        {
            var uri = authorizationService.GetAuthorizationUri();
            return Task.FromResult<IActionResult>(new RedirectResult(uri.AbsoluteUri, false));
        }

        public async Task<IActionResult> HandleCallbackAsync(HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var code = request.Query["code"];
            var token = await authorizationService.GetTokenAsync(code).ConfigureAwait(false);
            var profile = await libraryService.GetProfileAsync(token).ConfigureAwait(false);
            token.Id = profile.Id;
            var user = new User(profile.Id, profile.Type, token.RefreshToken);
            await userStore.SaveUserAsync(user).ConfigureAwait(false);
            await tokenStore.SaveTokenAsync(token).ConfigureAwait(false);
            return new OkResult();
        }
    }
}
