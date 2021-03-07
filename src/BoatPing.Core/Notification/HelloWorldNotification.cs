using System;
using System.Collections.Generic;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Notification
{
    /// <summary>
    /// notification that can be used to test the bot.
    /// </summary>
    public class HelloNotification : INotification
    {
        /// <summary>
        /// notification that can be used to test the bot.
        /// </summary>
        public HelloNotification()
        {

        }

        public IAd Ad()
        {
            return new FkAd("www.google.de", "", 0, "", new Dictionary<string, string>());
        }

        public string Event()
        {
            return "new-boat";
        }

        public string Title()
        {
            return "";
        }

        public string Content()
        {
            return $"⛵️👋 Hello there! I confirm that I am alive. But I found nothing to search for you. Put some search url in your searches.cfg file.";
        }
    }
}
