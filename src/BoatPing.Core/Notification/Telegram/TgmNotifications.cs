using System;
using System.IO;
using System.Linq;
using Telegram.Bot;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Notification.Telegram
{
    /// <summary>
    /// Notifications over Telegram.
    /// </summary>
    public sealed class TgmNotifications : INotifications
    {
        private readonly IText token;
        private readonly Uri chatsStorage;

        /// <summary>
        /// Notifications over Telegram.
        /// </summary>
        public TgmNotifications(string token, Uri chatsStorage) : this(new TextOf(token), chatsStorage)
        { }

        /// <summary>
        /// Notifications over Telegram.
        /// </summary>
        public TgmNotifications(IText token, Uri chatsStorage)
        {
            this.token = token;
            this.chatsStorage = chatsStorage;
        }

        public void Post(INotification notification)
        {
            var botClient = new TelegramBotClient(this.token.AsString());
            var updates = botClient.GetUpdatesAsync().Result; //fetch to allow sending

            foreach (var update in updates)
            {
                if(!File.Exists(this.chatsStorage.AbsolutePath))
                {
                    File.Create(this.chatsStorage.AbsolutePath).Close();
                }
                if (!File.ReadAllLines(this.chatsStorage.AbsolutePath).Contains(update.Message.Chat.Id.ToString()))
                {
                    
                    File.AppendAllLines(this.chatsStorage.AbsolutePath, new ManyOf(update.Message.Chat.Id.ToString()));
                }
            }

            foreach(var chat in File.ReadAllLines(chatsStorage.AbsolutePath))
            {
                if (chat.Length > 0)
                {
                    var chatId = Convert.ToInt32(chat.Trim());
                    botClient.SendTextMessageAsync(
                        new global::Telegram.Bot.Types.ChatId(chatId),
                        notification.Content()
                    );
                }
            }
        }
    }
}
