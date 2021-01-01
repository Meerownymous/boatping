using System;
using Telegram.Bot;

namespace BoatPing.Core.Notification.Telegram
{
    /// <summary>
    /// Notifications over Telegram.
    /// </summary>
    public sealed class TgmNotifications : INotifications
    {
        /// <summary>
        /// Notifications over Telegram.
        /// </summary>
        public TgmNotifications()
        { }

        public void Post(INotification notification)
        {
            var botClient = new TelegramBotClient("1468952780:AAFSSKQ6gTulFkVuUyYJ7TX_Ra8Uo51La0E");
            var updates = botClient.GetUpdatesAsync().Result; //fetch to allow sending

            botClient.SendTextMessageAsync(
                new global::Telegram.Bot.Types.ChatId(-414451065),
                new NewBoatText(notification.Ad()).AsString()
            );
        }
    }
}
