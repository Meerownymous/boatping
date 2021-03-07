using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using BoatPing.Core;
using BoatPing.Core.Ad.Scanboat;
using BoatPing.Core.LogBook;
using BoatPing.Core.Notification;
using BoatPing.Core.Notification.Telegram;
using Microsoft.VisualBasic;
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

            var cfgSearches = new Uri(Path.Combine(path, "memory", "searches.cfg"));
            var cfgBot = new Uri(Path.Combine(path, "memory", "bot.cfg"));

            var logbook = new FileLogBook(Path.Combine(path, "memory"));
            var fileNotifications = new FileNotifications(Path.Combine(path, "memory"));
            var telegramNotifications =
                new TgmNotifications(
                    new CfgBot(
                        new Uri(Path.Combine(path, "memory", "bot.cfg")),
                        error => LogError(path, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} bot.cfg:{error}")
                    )
                );
            var searches =
                new SourcesAds(
                    new CfgSearches(
                        new Uri(Path.Combine(path, "memory", "searches.cfg")),
                        new ListOf<string>("bandofboats.com", "boot24.com", "boat24.com", "scanboat.com"),
                        (error) => LogError(path, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {error}")
                    ),
                    new CfgPrice(new Uri(Path.Combine(path, "memory", "searches.cfg")), true, error => LogError(path, $"searches.cfg: {error}")).AsInt(),
                    new CfgPrice(new Uri(Path.Combine(path, "memory", "searches.cfg")), false, error => LogError(path, $"searches.cfg: {error}")).AsInt()
                );

            while (true)
            {
                try
                {
                    var stats = new Dictionary<string, int>();
                    stats["notifications"] = 0;
                    stats["price-changes"] = 0;
                    stats["new-boats"] = 0;
                    stats["overall-ads"] = 0;

                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    foreach (var source in searches)
                    {
                        if (!stats.ContainsKey($"source.{source.ToString()}"))
                        {
                            stats[$"source.{source.ToString()}"] = 0;
                        }
                        try
                        {

                            foreach (var ad in source)
                            {
                                stats["overall-ads"]++;
                                stats[$"source.{source.ToString()}"]++;
                                try
                                {
                                    if (!logbook.Contains(ad))
                                    {
                                        stats["notifications"]++;
                                        Thread.Sleep(new TimeSpan(0, 0, 1));
                                        telegramNotifications.Post(new NewBoatNotification(ad));
                                        fileNotifications.Post(new NewBoatNotification(ad));
                                        stats["new-boats"]++;
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
                                            stats["price-changes"]++;
                                        }

                                    }
                                    logbook.Record(ad);
                                }
                                catch (Exception ex)
                                {
                                    LogError(
                                        path,
                                        $"error scraping {ad.Url()}",
                                        ex.ToString()
                                    );
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogError(
                                path,
                                $"error while scraping {source}",
                                ex.ToString()
                            );
                        }
                    }
                    stopwatch.Stop();

                    LogStats(path, stats);
                }
                catch (Exception ex)
                {
                    LogError(
                        path,
                        "Error while going through searches:",
                        ex.ToString()

                    );
                }

                var waitTime = new CfgInterval(cfgBot, error => LogError(path, error)).Value();
                if (waitTime.TotalSeconds == 0)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(
                        new CfgInterval(cfgBot, error => LogError(path, error)).Value()
                    );
                }
            }
        }

        private static bool PriceChanged(IAd leftAd, IAd rightAd, int minPercentage)
        {
            var change = System.Math.Abs(leftAd.Price() - rightAd.Price());
            return change / leftAd.Price() * 100 > minPercentage;
        }

        private static void LogStats(string path, IDictionary<string, int> stats)
        {
            var message =
                $"Search completed. Overall ads {stats["overall-ads"]}, new boats {stats["new-boats"]}, price changes {stats["price-changes"]}";

            File.AppendAllLines(
                Path.Combine(path, "memory", "stats.log"),
                new Yaapii.Atoms.Enumerable.Mapped<string, string>(
                    msg => $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msg}",
                    new Yaapii.Atoms.Enumerable.Joined<string>(
                        new ManyOf(message),
                        new Yaapii.Atoms.Enumerable.Mapped<string,string>(
                            key => $"  {key.Replace("source.", "")}: {stats[key]} active ads",
                            new Filtered<string>(
                                key => key.StartsWith("source."),
                                stats.Keys
                            )
                        ) 
                    )
                )
            );
        }

        private static void LogError(string path, params string[] messages)
        {
            File.AppendAllLines(
                Path.Combine(path, "memory", "error.log"),
                new Yaapii.Atoms.Enumerable.Mapped<string, string>(
                    msg => $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msg}",
                    new ManyOf(messages)
                )
            );
        }

        private static void LogError(string path, Exception ex, params string[] messages)
        {
            File.AppendAllLines(
                Path.Combine(path, "memory", "error.log"),
                new Yaapii.Atoms.Enumerable.Mapped<string, string>(
                    msg => $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msg}",
                    messages
                )
            );

            File.AppendAllLines(
                Path.Combine(path, "memory", "error.log"),
                new Yaapii.Atoms.Enumerable.Mapped<string, string>(
                    msg => $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {msg}",
                    new ManyOf(ex.ToString())
                )
            );
        }
    }
}
