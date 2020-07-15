using System;
using System.Net.Http.Headers;
using System.Text;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Syncify.Core.Options;
using Syncify.Core.Providers;
using Syncify.Core.Services;
using Syncify.Core.Stores;

[assembly: FunctionsStartup(typeof(Syncify.Functions.Startup))]

namespace Syncify.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder?.Services.AddHttpClient();
            builder?.Services.AddDistributedMemoryCache();

            builder?.Services.AddOptions<SpotifyOptions>().Configure<IConfiguration>((settings, configuration) => configuration.GetSection("Spotify").Bind(settings));
            // builder?.Services.AddLogging(logging => logging.AddSentry("https://f7ebe79d27f1468596a32ca6f1225b5f@sentry.io/1792880"));

            builder?.Services.AddHttpClient<IAuthorizationService, AuthorizationService>((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<SpotifyOptions>>();
                var authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{options.Value?.ClientId}:{options.Value?.ClientSecret}"));
                client.BaseAddress = new Uri("https://accounts.spotify.com/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
            });
            builder?.Services.AddSingleton<IHttpClientService, HttpClientService>();
            builder?.Services.AddSingleton<ILibraryService, LibraryService>();
            builder?.Services.AddSingleton<IDatabaseService, DatabaseService>();

            builder?.Services.AddSingleton<IUserStore, UserStore>();
            builder?.Services.AddSingleton<ITokenStore, TokenStore>();
            builder?.Services.AddSingleton<ILibraryStore, LibraryStore>();

            builder?.Services.AddSingleton<ITokenProvider, TokenProvider>();
            builder?.Services.AddSingleton<IDatabaseProvider, DatabaseProvider>();

            builder?.Services.AddSingleton(provider =>
            {
                var environment = provider.GetRequiredService<IHostEnvironment>();
                var keyVault = provider.GetRequiredService<IConfiguration>().GetValue<string>("KeyVault");
                TokenCredential tokenCredential = environment.IsDevelopment() ? (TokenCredential)new EnvironmentCredential() : new DefaultAzureCredential();
                return new SecretClient(new Uri($"https://{keyVault}.vault.azure.net"), tokenCredential);
            });
            builder?.Services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Cosmos");
                return new CosmosClient(connectionString);
            });
        }
    }
}
