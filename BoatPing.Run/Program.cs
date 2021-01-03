using System;
using System.Diagnostics;
using System.IO;
using BoatPing.Core;
using BoatPing.Core.Ad.Boat24;
using BoatPing.Core.Ad.Scanboat;
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
                    var notifications = 0;
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    foreach (var ad in
                        new Joined<IAd>(
                            new ScbAds(new ScbSearch19701985(), 0, 59000),
                            new ScbAds(new ScbSearch19851995(), 0, 59000),
                            new ScbAds(new ScbSearch19952000(), 0, 59000),
                            new ScbAds(new ScbSearch20002015(), 0, 59000),
                            new BoaAds(new BoaDefaultSearch()),
                            new BoaAds("https://www.boat24.com/en/sailboats/?src=&typ%5B%5D=230&page=0&cem=&whr=EUR&prs_min=30000&prs_max=59000&lge_min=10&lge_max=14&bre_min=&bre_max=&tie_min=&tie_max=&gew_min=&gew_max=&jhr_min=1970&jhr_max=&lei_min=&lei_max=&ant=&rgo%5B%5D=11&rgo%5B%5D=49&rgo%5B%5D=43&rgo%5B%5D=24&kie=&mob=&per_min=&per_max=&cab_min=&cab_max=&ber_min=&ber_max=&hdr_min=&hdr_max=&sort=rand"),
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
                                notifications++;
                            }
                        }
                        catch (Exception ex)
                        {
                            File.AppendAllLines(
                                Path.Combine(path, "memory", "error.log"),
                                new ManyOf(
                                    $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} error scraping {ad.Url().ToString()}",
                                    ex.ToString()
                                )
                            );
                        }
                    }
                    stopwatch.Stop();
                    File.AppendAllLines(
                        Path.Combine(path, "memory", "scrapes.log"),
                        new ManyOf(
                            $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Scraped {ads} ads and sent {notifications} notifications in {stopwatch.Elapsed.TotalSeconds}s"
                        )
                    );

                }
                catch (Exception ex)
                {
                    File.AppendAllLines(
                        Path.Combine(path, "memory", "error.log"),
                        new ManyOf(
                            "error while scraping",
                            ex.ToString()
                        )
                    );
                }
                System.Threading.Thread.Sleep(new TimeSpan(0, 30, 0));
            }
        }
    }
}
