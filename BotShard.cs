using DiscordBotBase.Entities;
using DiscordBotBase.Logic;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotBase
{
    public class BotShard
    {
        public int ShardId { get; private set; }

        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public SharedData SharedData { get; set; }
        public Settings Settings { get; }

        public BotShard(Settings settings, int id, SharedData sharedData)
        {
            Settings = settings;
            SharedData = sharedData;
            ShardId = id;
        }

        internal void Initialize()
        {

            var cfg = new DiscordConfiguration
            {
                Token = Settings.Token,
                MinimumLogLevel = LogLevel.Debug,
                ShardCount = Settings.ShardCount,
                ShardId = ShardId,
                MessageCacheSize = 2048,
                HttpTimeout = TimeSpan.FromSeconds(13)
            };

            Client = new DiscordClient(cfg);

            Client.ClientErrored += async (client, args) =>
            {
                Console.WriteLine(args.Exception);
                await Task.CompletedTask;
            };

            Interactivity = Client.UseInteractivity(new InteractivityConfiguration
            {

            });

            var deps = new ServiceCollection()
                .AddSingleton(SharedData)
                .AddSingleton(Interactivity)
                .BuildServiceProvider();

            Commands = Client.UseCommandsNext(new CommandsNextConfiguration
            {
                CaseSensitive = false,
                EnableDefaultHelp = true,
                EnableDms = false,
                EnableMentionPrefix = true,
                PrefixResolver = GetPrefixLengthAsync,
                Services = deps,
            });

            Commands.RegisterCommands(Assembly.GetExecutingAssembly());         

            DiscordEventHandler.Install(Client, this);
        }

        public Task RunAsync() =>
            Client.ConnectAsync();

        internal async Task DisconnectAndDispose()
        {
            await Client.DisconnectAsync();
            Client.Dispose();
        }

        public async Task<int> GetPrefixLengthAsync(DiscordMessage msg)
        {
            return await Task.FromResult(msg.GetStringPrefixLength(Settings.Prefix));
        }
    }
}
