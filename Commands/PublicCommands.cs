using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBotBase.Entities;
using DiscordBotBase.Logic.Extensions;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DiscordBotBase.Commands
{
    [ModuleLifespan(ModuleLifespan.Transient)]
    public class PublicCommands : BaseCommandModule
    {
        private readonly SharedData Shared;

        public PublicCommands(SharedData data)
        {
            Shared = data;
        }

        [Command("snipe")]
        [Aliases("s")]
        [Description("Snipes last sent message")]
        public async Task SnipeAsync(CommandContext ctx)
        {
            if (Shared.DeletedMessages.ContainsKey(ctx.Channel.Id))
            {
                var mainRole = ctx.Guild.CurrentMember.Roles.OrderByDescending(r => r.Position).First();
                var m = Shared.DeletedMessages[ctx.Channel.Id];

                var content = m.Content;
                if (content.Length > 500)
                    content = content.Substring(0, 500) + "...";

                var emb = new DiscordEmbedBuilder()
                    .WithColor(mainRole.Color)
                    .WithAuthor($"{m.Author.ToDiscordTag()}", iconUrl: m.Author.GetAvatarUrl(ImageFormat.Png));

                if (!string.IsNullOrEmpty(m.Content))
                {
                    emb.WithDescription(m.Content);
                    emb.WithTimestamp(m.Id);
                }

                await ctx.RespondAsync(embed: emb.Build()).ConfigureAwait(false);
                return;
            }
            await ctx.RespondAsync(content: "No message to snipe!").ConfigureAwait(false);
        }

        [Command("snipeedit")]
        [Aliases("se", "sedit")]
        [Description("Snipes last edited message")]
        public async Task SnipeEditAsync(CommandContext ctx)
        {
            if (Shared.EditedMessages.ContainsKey(ctx.Channel.Id))
            {
                var mainRole = ctx.Guild.CurrentMember.Roles.OrderByDescending(r => r.Position).First();
                var m = Shared.EditedMessages[ctx.Channel.Id];
                var content = m.Content;

                if (content.Length > 500)
                    content = content.Substring(0, 500) + "...";

                var emb = new DiscordEmbedBuilder()
                    .WithColor(mainRole.Color)
                    .WithAuthor($"{m.Author.ToDiscordTag()}", iconUrl: m.Author.GetAvatarUrl(ImageFormat.Png));

                if (!string.IsNullOrEmpty(m.Content))
                {
                    emb.WithDescription(m.Content);
                    emb.WithTimestamp(m.Id);
                }

                await ctx.RespondAsync(embed: emb.Build()).ConfigureAwait(false);
                return;
            }
            await ctx.RespondAsync(content: "No message to snipe!").ConfigureAwait(false);
        }
    }
}
