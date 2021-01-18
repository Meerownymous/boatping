using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using BoatPing.Core;
using BoatPing.Core.Ad.BandOfBoats;
using BoatPing.Core.Ad.Boat24;
using BoatPing.Core.Ad.Scanboat;
using BoatPing.Core.Boot24;
using BoatPing.Core.LogBook;
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
                    var newBoats = 0;
                    var priceChanges = 0;
                    var notifications = 0;
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    foreach (var ad in
                        new Joined<IAd>(
                            //DE
                            new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=DE&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max="),
                            //NL
                            new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=NL&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14beam_min=&beam_max=&horse_power_min=&horse_power_max="),
                            //BE
                            new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=BE&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max="),
                            //FR
                            new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=FR&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max="),
                            //DK
                            new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=DK&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max="),
                            //UK
                            new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=GB&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max="),
                            //NW
                            new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=NO&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max="),
                            //SE
                            new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=SE&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max="),
                            new ScbAds(new ScbSearch19701985(), 0, 59000),
                            new ScbAds(new ScbSearch19851995(), 0, 59000),
                            new ScbAds(new ScbSearch19952000(), 0, 59000),
                            new ScbAds(new ScbSearch20002015(), 0, 59000),
                            new BoaAds(new BoaDefaultSearch()),
                            new BoaAds("https://www.boat24.com/en/sailboats/?src=&typ%5B%5D=230&page=0&cem=&whr=EUR&prs_min=20000&prs_max=59000&lge_min=10&lge_max=14&bre_min=&bre_max=&tie_min=&tie_max=&gew_min=&gew_max=&jhr_min=1970&jhr_max=&lei_min=&lei_max=&ant=&rgo%5B%5D=11&rgo%5B%5D=49&rgo%5B%5D=43&rgo%5B%5D=24&kie=&mob=&per_min=&per_max=&cab_min=&cab_max=&ber_min=&ber_max=&hdr_min=&hdr_max=&sort=rand"),
                            new B24Ads(new B24DefaultSearch()),
                            new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c25")
                        )
                    )
                    {
                        ads++;
                        try
                        {
                            if (!logbook.Contains(ad))
                            {
                                notifications++;
                                Thread.Sleep(new TimeSpan(0, 0, 1));
                                telegramNotifications.Post(new NewBoatNotification(ad));
                                fileNotifications.Post(new NewBoatNotification(ad));
                                newBoats++;
                            }
                            else
                            {
                                var lastAd =
                                    new LastOf<IAd>(
                                        logbook.RecordsOf(ad)
                                    ).Value();
                                if (lastAd.Price() != ad.Price())
                                {
                                    telegramNotifications.Post(new PriceChangedNotification(ad, lastAd));
                                    fileNotifications.Post(new PriceChangedNotification(ad, lastAd));
                                    priceChanges++;
                                }
                                
                            }
                            logbook.Record(ad);
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
                            $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Scraped {ads} ads. Found {newBoats} new boats and {priceChanges} price changes in {stopwatch.Elapsed.TotalSeconds}s"
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
