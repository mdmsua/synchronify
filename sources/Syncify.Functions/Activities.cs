using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Syncify.Core.Models;
using Syncify.Core.Services;
using Syncify.Core.Stores;

namespace Syncify.Functions
{
    public class Activities
    {
        private readonly IUserStore userStore;
        private readonly ITokenStore tokenStore;
        private readonly ILibraryService libraryService;
        private readonly ILibraryStore libraryStore;
        private readonly IDatabaseService databaseService;

        public Activities(IUserStore userStore, ITokenStore tokenStore, ILibraryService libraryService, ILibraryStore libraryStore, IDatabaseService databaseService)
        {
            this.userStore = userStore;
            this.tokenStore = tokenStore;
            this.libraryService = libraryService;
            this.libraryStore = libraryStore;
            this.databaseService = databaseService;
        }

        [FunctionName(nameof(Users))]
        public async Task<User[]> Users([ActivityTrigger] object @object)
        {
            var users = new List<User>();
            await foreach (var user in userStore.GetUsersAsync())
            {
                users.Add(user);
            }

            return users.ToArray();
        }

        [FunctionName(nameof(Backup))]
        public Task Backup([ActivityTrigger] string id) =>
            databaseService.BackupDatabaseAsync(id);

        [FunctionName(nameof(Drop))]
        public Task Drop([ActivityTrigger] string id) =>
            databaseService.DropDatabaseAsync(id);

        [FunctionName(nameof(Token))]
        public Task Token([ActivityTrigger] string id) =>
            tokenStore.GetTokenAsync(id);

        [FunctionName(nameof(Tracks))]
        public async Task<int> Tracks([ActivityTrigger] IDurableActivityContext context)
        {
            var token = context.GetInput<Token>();
            var tracks = await libraryService.GetTracksAsync(token);
            return tracks.Count;
        }

        [FunctionName(nameof(Albums))]
        public Task<int> Albums([ActivityTrigger] IDurableActivityContext context)
        {
            return Task.FromResult(0);
        }

        [FunctionName(nameof(Playlists))]
        public Task<int> Playlists([ActivityTrigger] IDurableActivityContext context)
        {
            return Task.FromResult(0);
        }

        [FunctionName(nameof(Log))]
        public async Task Log([ActivityTrigger] IDurableActivityContext context)
        {
            var (id, started, finished, tracks, albums, playlists) = context.GetInput<(string, DateTime, DateTime, int, int, int)>();
        }
    }
}
