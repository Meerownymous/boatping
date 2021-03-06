using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using BoatPing.Core;
using BoatPing.Core.Ad.BandOfBoats;
using BoatPing.Core.Ad.Boat24;
using BoatPing.Core.Ad.Scanboat;
using BoatPing.Core.Boot24;
using BoatPing.Core.LogBook;
using BoatPing.Core.Notification;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Map;

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
                    foreach (var source in Sources())
                    {
                        try
                        {
                            foreach (var ad in source.Value)
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
                                        if (PriceChanged(ad, lastAd, 2))
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
                        }
                        catch(Exception ex)
                        {
                            File.AppendAllLines(
                                Path.Combine(path, "memory", "error.log"),
                                new ManyOf(
                                    $"error while scraping {source.Key}",
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

        private static IDictionary<string,IEnumerable<IAd>> Sources()
        {
            return
                new MapOf<IEnumerable<IAd>>(
                    //new KvpOf<IEnumerable<IAd>>("band of boats DE", new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=DE&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=")),
                    //new KvpOf<IEnumerable<IAd>>("band of boats NL", new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=NL&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14beam_min=&beam_max=&horse_power_min=&horse_power_max=")),
                    //new KvpOf<IEnumerable<IAd>>("band of boats BE", new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=BE&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=")),
                    //new KvpOf<IEnumerable<IAd>>("band of boats FR", new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=FR&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=")),
                    //new KvpOf<IEnumerable<IAd>>("band of boats DK", new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=DK&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=")),
                    //new KvpOf<IEnumerable<IAd>>("band of boats UK", new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=GB&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=")),
                    //new KvpOf<IEnumerable<IAd>>("band of boats NW", new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=NO&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=")),
                    //new KvpOf<IEnumerable<IAd>>("band of boats SE", new BobAds("https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=SE&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=")),
                    new KvpOf<IEnumerable<IAd>>("scanboat 1970-1985", new ScbAds(new ScbSearch19701985(), 0, 59000)),
                    new KvpOf<IEnumerable<IAd>>("scanboat 1985-1995", new ScbAds(new ScbSearch19851995(), 0, 59000)),
                    new KvpOf<IEnumerable<IAd>>("scanboat 1995-2000", new ScbAds(new ScbSearch19952000(), 0, 59000)),
                    new KvpOf<IEnumerable<IAd>>("scanboat 2000-2015", new ScbAds(new ScbSearch20002015(), 0, 59000))
                    //new KvpOf<IEnumerable<IAd>>("boat24 DE FR NL DK BE", new BoaAds(new BoaDefaultSearch())),
                    //new KvpOf<IEnumerable<IAd>>("boat24 BE SW NW UK", new BoaAds("https://www.boat24.com/en/sailboats/?src=&typ%5B%5D=230&page=0&cem=&whr=EUR&prs_min=20000&prs_max=59000&lge_min=10&lge_max=14&bre_min=&bre_max=&tie_min=&tie_max=&gew_min=&gew_max=&jhr_min=1970&jhr_max=&lei_min=&lei_max=&ant=&rgo%5B%5D=11&rgo%5B%5D=49&rgo%5B%5D=43&rgo%5B%5D=24&kie=&mob=&per_min=&per_max=&cab_min=&cab_max=&ber_min=&ber_max=&hdr_min=&hdr_max=&sort=rand")),
                    //new KvpOf<IEnumerable<IAd>>("boot24 DE", new B24Ads(new B24DefaultSearch())),
                    //new KvpOf<IEnumerable<IAd>>("boot24 NL", new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c25")),
                    //new KvpOf<IEnumerable<IAd>>("boot24 FR", new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c10")),
                    //new KvpOf<IEnumerable<IAd>>("boot24 GB", new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c39")),
                    //new KvpOf<IEnumerable<IAd>>("boot24 PL", new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c27")),
                    //new KvpOf<IEnumerable<IAd>>("boot24 BE", new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c5")),
                    //new KvpOf<IEnumerable<IAd>>("boot24 DK", new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c8")),
                    //new KvpOf<IEnumerable<IAd>>("boot24 IR", new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c13")),
                    //new KvpOf<IEnumerable<IAd>>("boot24 NW", new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c6")),
                    //new KvpOf<IEnumerable<IAd>>("boot24 SW", new B24Ads("https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c31"))
            );
        }

        private static bool PriceChanged(IAd leftAd, IAd rightAd, int minPercentage)
        {
            var change = System.Math.Abs(leftAd.Price() - rightAd.Price());
            return change / leftAd.Price() * 100 > minPercentage;
        }
    }
}
