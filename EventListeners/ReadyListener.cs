using DiscordBotBase.Logic;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotBase.EventListeners
{
    public class ReadyListener
    {
        [DiscordEvent(EventType.Ready)]
        public static async Task ReadyAsync(BotShard bot, DiscordClient client, ReadyEventArgs args)
        {
            Console.WriteLine($"Logged into {client.CurrentUser.Username}");
            await client.UpdateStatusAsync(new DiscordActivity
            {
                Name = $"over {bot.Settings.ShardCount} shard" + (bot.Settings.ShardCount > 1 ? "s!" : "!"),
                ActivityType = ActivityType.Watching
            });
        }

    }
}
