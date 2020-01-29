using System;
using System.Net.Http.Headers;
using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Syncify.Options;
using Syncify.Stores;
using Syncify.Services;
using Syncify.Handlers;
using Azure.Core;
using Syncify.Providers;

[assembly: FunctionsStartup(typeof(Syncify.Startup))]

namespace Syncify
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddMemoryCache();
            builder.Services.AddOptions<SpotifyOptions>().Configure<IConfiguration>((settings, configuration) => configuration.GetSection("Spotify").Bind(settings));
            builder.Services.AddOptions<KeyVaultOptions>().Configure<IConfiguration>((settings, configuration) => configuration.GetSection("KeyVault").Bind(settings));
            builder.Services.AddLogging(logging => logging.AddSentry("https://f7ebe79d27f1468596a32ca6f1225b5f@sentry.io/1792880"));
            builder.Services.AddHttpClient<IAuthorizationService, AuthorizationService>((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<SpotifyOptions>>();
                var authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{options.Value?.ClientId}:{options.Value?.ClientSecret}"));
                client.BaseAddress = new Uri("https://accounts.spotify.com/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
            });
            builder.Services.AddSingleton<IMongoClient>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Mongo");
                return new MongoClient(connectionString);
            });
            builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
            builder.Services.AddSingleton<ILibraryService, LibraryService>();
            builder.Services.AddSingleton<ITokenStore, TokenStore>();
            builder.Services.AddSingleton<ITokenProvider, TokenProvider>();
            builder.Services.AddSingleton<IUserStore, UserStore>();
            builder.Services.AddSingleton<IHttpHandler, HttpHandler>();
            builder.Services.AddSingleton<ICronHandler, CronHandler>();
            builder.Services.AddSingleton(provider =>
            {
                var options = provider.GetRequiredService<IOptions<KeyVaultOptions>>();
                var isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
                TokenCredential tokenCredential = isProduction ? new DefaultAzureCredential() : (TokenCredential)new EnvironmentCredential();
                return new SecretClient(new Uri($"https://{options.Value.Name}.vault.azure.net"), tokenCredential);
            });
            builder.Services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Storage");
                return CloudStorageAccount.Parse(connectionString).CreateCloudTableClient();
            });
        }
    }
}
