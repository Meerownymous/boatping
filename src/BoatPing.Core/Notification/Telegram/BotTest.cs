using System;
using BoatPing.Core.Model;
using BoatPing.Core.Notification;
using Telegram.Bot;
using Telegram.Bot.Types;
using Xunit;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Notification.Telegram.Test
{
    public sealed class BotTest
    {
        [Fact]
        public void SendsMessage()
        {

            var botClient = new TelegramBotClient("1680123742:AAH6vH0FeMLAr5BLi9XIrmC_CklE2ASAjZE");
            var updates = botClient.GetUpdatesAsync().Result;

            botClient.SendTextMessageAsync(
                new global::Telegram.Bot.Types.ChatId(-414451065),
                new NewBoatText(new SimpleAd("123", "BOATS BOATS BOATS", "http://www.boot24.de", 0.00, new MapOf("title", "This is not a boat. Do not click on this, because it is not a boat. NOT A BOAT"))).AsString()
            );
        }
    }
}
