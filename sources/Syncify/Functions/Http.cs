using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Syncify.Services;
using Syncify.Repositories;

namespace Syncify.Functions
{
    public class Http
    {
        private readonly IAuthorizationService authorizationService;
        private readonly ILibraryService libraryService;
        private readonly ITokenRepository tokenRepository;

        public Http(IAuthorizationService authorizationService, ILibraryService libraryService, ITokenRepository tokenRepository)
        {
            this.authorizationService = authorizationService;
            this.libraryService = libraryService;
            this.tokenRepository = tokenRepository;
        }

        [FunctionName("Authorize")]
        public IActionResult Authorize(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "auth")] HttpRequest req,
            ILogger log)
        {
            var uri = authorizationService.GetAuthorizationUri();
            return new RedirectResult(uri.AbsoluteUri, false);
        }

        [FunctionName("Callback")]
        public async Task<IActionResult> Callback(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "auth/callback")] HttpRequest req,
            ILogger log)
        {
            var code = req.Query["code"];
            var token = await authorizationService.GetTokenAsync(code);
            var profile = await libraryService.GetProfileAsync(token);
            token.Id = profile.Id;
            await tokenRepository.SaveTokenAsync(token);
            return new OkResult();
        }
    }
}
