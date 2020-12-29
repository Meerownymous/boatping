using System;
using System.IO;
using BoatPing.Core.Boot24;
using BoatPing.Core.Notification;

namespace BoatPing.Run
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                var path = Environment.CurrentDirectory;
                var logbook = new BoatPing.Core.LogBook.FileLogBook(Path.Combine(path, "memory"));
                var notifications = new Core.FileNotifications(Path.Combine(path, "memory"));

                foreach (var ad in new B24Ads(new B24DefaultSearch()))
                {
                    try
                    {
                        if (!logbook.Contains(ad))
                        {
                            notifications.Post(new NewBoatNotification(ad));
                            logbook.Record(ad);
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
                System.Threading.Thread.Sleep(new TimeSpan(0, 30, 0));
            }
        }
    }
}
