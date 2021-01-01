using System;
using System.Diagnostics;
using System.IO;
using BoatPing.Core;
using BoatPing.Core.Boot24;
using BoatPing.Core.Notification;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory;
            var logbook = new BoatPing.Core.LogBook.FileLogBook(Path.Combine(path, "memory"));
            var fileNotifications = new Core.FileNotifications(Path.Combine(path, "memory"));
            var telegramNotifications = new Core.Notification.Telegram.TgmNotifications();

            while (true)
            {
                

                try
                {
                    var ads = 0;
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    foreach (var ad in
                        new Joined<IAd>(
                            new B24Ads(new B24DefaultSearch()),
                            new B24Ads("https://www.boot24.com/segelboot/#pab=30000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c25")
                        )
                    )
                    {
                        ads++;
                        try
                        {
                            if (!logbook.Contains(ad))
                            {
                                telegramNotifications.Post(new NewBoatNotification(ad));
                                fileNotifications.Post(new NewBoatNotification(ad));
                                logbook.Record(ad);
                            }
                        }
                        catch (Exception ex)
                        {
                            File.AppendAllLines(
                                Path.Combine(path, "memory", "error.log"),
                                new ManyOf(
                                    $"error scraping {ad.Url().ToString()}",
                                    ex.ToString()
                                )
                            );
                        }
                    }
                    stopwatch.Stop();
                    File.AppendAllLines(
                        Path.Combine(path, "memory", "scrapes.log"),
                        new ManyOf(
                            $"{DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss")}[] Scraped {ads} ads of {new B24DefaultSearch().AsString()} in {stopwatch.Elapsed.TotalSeconds}s"
                        )
                    );

                }
                catch (Exception ex)
                {
                    File.AppendAllLines(
                        Path.Combine(path, "memory", "error.log"),
                        new ManyOf(
                            "error scraping boot24",
                            ex.ToString()
                        )
                    );
                }
                System.Threading.Thread.Sleep(new TimeSpan(0, 30, 0));
            }
        }
    }
}
