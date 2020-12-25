using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DiscordBotBase.Entities
{
    public class Settings
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public int ShardCount { get; set; }
    }
}
