using System;
using Telegram.Bot;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Notification.Telegram
{
    /// <summary>
    /// Notifications over Telegram.
    /// </summary>
    public sealed class TgmNotifications : INotifications
    {
        private readonly IText token;

        /// <summary>
        /// Notifications over Telegram.
        /// </summary>
        public TgmNotifications(string token) : this(new TextOf(token))
        { }

        /// <summary>
        /// Notifications over Telegram.
        /// </summary>
        public TgmNotifications(IText token)
        {
            this.token = token;
        }

        public void Post(INotification notification)
        {
            var botClient = new TelegramBotClient(this.token.AsString());
            var updates = botClient.GetUpdatesAsync().Result; //fetch to allow sending

            botClient.SendTextMessageAsync(
                new global::Telegram.Bot.Types.ChatId(-414451065),
                notification.Content()
            );
        }
    }
}
