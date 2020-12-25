using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;

namespace DiscordBotBase.Logic
{
    [AttributeUsage(AttributeTargets.Method)]

    public class DiscordEventAttribute : Attribute
    {
        public EventType Target { get; }

        public DiscordEventAttribute(EventType target)
            => Target = target;

        public void Register(BotShard bot, DiscordClient client, MethodInfo listener)
        {
            Task OnEventWithArgs(object s, object e)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await (Task)listener.Invoke(null, new[] { bot, s, e });
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync($"[Listener] Uncaught exception in listener thread: {ex}").ConfigureAwait(false);
                        await Console.Out.WriteLineAsync($"{ex.Message} {ex.StackTrace}").ConfigureAwait(false);
                    }
                });
                return Task.CompletedTask;
            }

            Task OnEventVoid()
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await (Task)listener.Invoke(null, new object[] { bot });
                    }
                    catch (Exception ex)
                    {
                        await Console.Out.WriteLineAsync($"[Listener] Uncaught exception in listener thread: {ex}").ConfigureAwait(false);
                        await Console.Out.WriteLineAsync($"{ex.Message} {ex.StackTrace}").ConfigureAwait(false);
                    }
                });
                return Task.CompletedTask;
            }

            switch (Target)
            {
                case EventType.ClientErrored:
                    client.ClientErrored += OnEventWithArgs;
                    break;
                case EventType.SocketErrored:
                    client.SocketErrored += OnEventWithArgs;
                    break;
                case EventType.SocketOpened:
                    client.SocketOpened += OnEventWithArgs;
                    break;
                case EventType.SocketClosed:
                    client.SocketClosed += OnEventWithArgs;
                    break;
                case EventType.Ready:
                    client.Ready += OnEventWithArgs;
                    break;
                case EventType.Resumed:
                    client.Resumed += OnEventWithArgs;
                    break;
                case EventType.ChannelCreated:
                    client.ChannelCreated += OnEventWithArgs;
                    break;
                case EventType.DmChannelCreated:
                    client.DmChannelCreated += OnEventWithArgs;
                    break;
                case EventType.ChannelUpdated:
                    client.ChannelUpdated += OnEventWithArgs;
                    break;
                case EventType.ChannelDeleted:
                    client.ChannelDeleted += OnEventWithArgs;
                    break;
                case EventType.DmChannelDeleted:
                    client.DmChannelDeleted += OnEventWithArgs;
                    break;
                case EventType.ChannelPinsUpdated:
                    client.ChannelPinsUpdated += OnEventWithArgs;
                    break;
                case EventType.GuildCreated:
                    client.GuildCreated += OnEventWithArgs;
                    break;
                case EventType.GuildAvailable:
                    client.GuildAvailable += OnEventWithArgs;
                    break;
                case EventType.GuildUpdated:
                    client.GuildUpdated += OnEventWithArgs;
                    break;
                case EventType.GuildDeleted:
                    client.GuildDeleted += OnEventWithArgs;
                    break;
                case EventType.GuildUnavailable:
                    client.GuildUnavailable += OnEventWithArgs;
                    break;
                case EventType.MessageCreated:
                    client.MessageCreated += OnEventWithArgs;
                    break;
                case EventType.PresenceUpdated:
                    client.PresenceUpdated += OnEventWithArgs;
                    break;
                case EventType.GuildBanAdded:
                    client.GuildBanAdded += OnEventWithArgs;
                    break;
                case EventType.GuildBanRemoved:
                    client.GuildBanRemoved += OnEventWithArgs;
                    break;
                case EventType.GuildEmojisUpdated:
                    client.GuildEmojisUpdated += OnEventWithArgs;
                    break;
                case EventType.GuildIntegrationsUpdated:
                    client.GuildIntegrationsUpdated += OnEventWithArgs;
                    break;
                case EventType.GuildMemberAdded:
                    client.GuildMemberAdded += OnEventWithArgs;
                    break;
                case EventType.GuildMemberRemoved:
                    client.GuildMemberRemoved += OnEventWithArgs;
                    break;
                case EventType.GuildMemberUpdated:
                    client.GuildMemberUpdated += OnEventWithArgs;
                    break;
                case EventType.GuildRoleCreated:
                    client.GuildRoleCreated += OnEventWithArgs;
                    break;
                case EventType.GuildRoleUpdated:
                    client.GuildRoleUpdated += OnEventWithArgs;
                    break;
                case EventType.GuildRoleDeleted:
                    client.GuildRoleDeleted += OnEventWithArgs;
                    break;
                case EventType.InviteCreated:
                    client.InviteCreated += OnEventWithArgs;
                    break;
                case EventType.InviteDeleted:
                    client.InviteDeleted += OnEventWithArgs;
                    break;
                case EventType.MessageAcknowledged:
                    client.MessageAcknowledged += OnEventWithArgs;
                    break;
                case EventType.MessageUpdated:
                    client.MessageUpdated += OnEventWithArgs;
                    break;
                case EventType.MessageDeleted:
                    client.MessageDeleted += OnEventWithArgs;
                    break;
                case EventType.MessagesBulkDeleted:
                    client.MessagesBulkDeleted += OnEventWithArgs;
                    break;
                case EventType.TypingStarted:
                    client.TypingStarted += OnEventWithArgs;
                    break;
                case EventType.UserSettingsUpdated:
                    client.UserSettingsUpdated += OnEventWithArgs;
                    break;
                case EventType.UserUpdated:
                    client.UserUpdated += OnEventWithArgs;
                    break;
                case EventType.VoiceStateUpdated:
                    client.VoiceStateUpdated += OnEventWithArgs;
                    break;
                case EventType.VoiceServerUpdated:
                    client.VoiceServerUpdated += OnEventWithArgs;
                    break;
                case EventType.GuildMembersChunked:
                    client.GuildMembersChunked += OnEventWithArgs;
                    break;
                case EventType.UnknownEvent:
                    client.UnknownEvent += OnEventWithArgs;
                    break;
                case EventType.MessageReactionAdded:
                    client.MessageReactionAdded += OnEventWithArgs;
                    break;
                case EventType.MessageReactionRemoved:
                    client.MessageReactionRemoved += OnEventWithArgs;
                    break;
                case EventType.MessageReactionsCleared:
                    client.MessageReactionsCleared += OnEventWithArgs;
                    break;
                case EventType.WebhooksUpdated:
                    client.WebhooksUpdated += OnEventWithArgs;
                    break;
                case EventType.Heartbeated:
                    client.Heartbeated += OnEventWithArgs;
                    break;
                case EventType.CommandExecuted:
                    bot.Commands.CommandErrored += OnEventWithArgs;
                    break;
                case EventType.CommandErrored:
                    bot.Commands.CommandErrored += OnEventWithArgs;
                    break;
            }
        }
    }
}
