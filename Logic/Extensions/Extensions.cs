using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotBase.Logic.Extensions
{
    public static class Extensions
    {
        public static string ToDiscordTag(this DiscordUser user)
            => $"{user.Username}#{user.Discriminator}";
    }
}
