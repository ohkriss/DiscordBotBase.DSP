using DiscordBotBase.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotBase
{
    public class Bot
    {
        public Settings Settings { get; private set; }
        public SharedData SharedData { get; private set; }
        public List<BotShard> Shards { get; private set; }

        private CancellationTokenSource CTS { get; set; }

        public async Task InitializeAsync(string[] args)
        {
            if (!File.Exists("settings.json"))
            {
                var json = JsonSerializer.Serialize(new Settings(), new() { WriteIndented = true});
                File.WriteAllText("settings.json", json, new UTF8Encoding(false));
                Console.WriteLine("Config file was not found, a new one was generated.");
                Console.ReadKey();
                return;
            }

            var input = File.ReadAllText("settings.json", new UTF8Encoding(false));
            Settings = JsonSerializer.Deserialize<Settings>(input);

            var newjson = JsonSerializer.Serialize(Settings, new() { WriteIndented = true });
            File.WriteAllText("settings.json", newjson, new UTF8Encoding(false));

            Shards = new List<BotShard>();
            InitializeSharedData(args);

            for (var i = 0; i < Settings.ShardCount; i++)
            {
                var shard = new BotShard(Settings, i, SharedData);
                shard.Initialize();
                Shards.Add(shard);
            }


            foreach (var shard in Shards)
                await shard.RunAsync();

            var token = CTS.Token;
            try
            {
                await Task.Delay(Timeout.Infinite, token);
            }
            catch (OperationCanceledException) { }

            foreach (var shard in Shards)
                await shard.DisconnectAndDispose();

            SharedData.CTS.Dispose();
        }

        private void InitializeSharedData(string[] args)
        {
            CTS = new CancellationTokenSource();
            SharedData = new SharedData
            {
                CTS = CTS,
                DefaultPrefix = Settings.Prefix,
                Bot = this
            };
        }
    }
}
