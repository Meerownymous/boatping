using System;

namespace BoatPing.Run
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            while (true)
            {
                var path = Environment.CurrentDirectory;
                var logbook = new BoatPing.Core.LogBook.FileLogBook(Path.Combine(path, "logbook"));
                var notifications = new Core.FileNotifications(Path.Combine(path, "notifications"));

                foreach (var ad in new B24Ads(new B24DefaultSearch()))
                {
                    if (!logbook.Contains(ad))
                    {
                        notifications.Post(new NewBoatNotification(ad));
                        logbook.Record(ad);
                    }
                }
                System.Threading.Thread.Sleep(new TimeSpan(0, 30, 0));
            }
        }
    }
}
