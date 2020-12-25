using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DiscordBotBase.Logic
{
    public class DiscordEventHandler
    {
        public static IEnumerable<DiscordEventMethod> ListenerMethods { get; private set; }

        public static void Install(DiscordClient client, BotShard bot)
        {
            ListenerMethods =
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                from m in t.GetMethods()
                let attribute = m.GetCustomAttribute(typeof(DiscordEventAttribute), true)
                where attribute != null
                select new DiscordEventMethod { Method = m, Attribute = attribute as DiscordEventAttribute, Type = t };

            foreach (var listener in ListenerMethods)
                listener.Attribute.Register(bot, client, listener.Method);
        }
    }

    public class DiscordEventMethod
    {
        public Type Type { get; internal set; }
        public MethodInfo Method { get; internal set; }
        public DiscordEventAttribute Attribute { get; internal set; }
    }
}
