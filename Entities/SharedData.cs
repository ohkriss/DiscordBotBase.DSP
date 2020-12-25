using DSharpPlus.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DiscordBotBase.Entities
{
    public class SharedData
    {
        public string DefaultPrefix { get; internal set; }
        public CancellationTokenSource CTS { get; set; }

        public ConcurrentDictionary<ulong, DiscordMessage> DeletedMessages { get; set; } = new();
        public ConcurrentDictionary<ulong, DiscordMessage> EditedMessages { get; set; } = new();

        public Bot Bot;
    }
}
