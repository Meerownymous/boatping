using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Ad.Scanboat
{
    /// <summary>
    /// A scanboat.com ad scraped directly from the search page.
    /// </summary>
    public class ScbAd : IAd
    {
        private readonly MapOf attributes;

        /// <summary>
        /// A scanboat.com ad scraped directly from the search page.
        /// </summary>
        public ScbAd(IWebElement adBox)
        {
            this.attributes =
                new MapOf(() =>
                {
                    var map =
                        new MapOf(
                            new KvpOf("source", "scanboat.com"),
                            new KvpOf("url", adBox.FindElement(By.TagName("a")).GetAttribute("href")),
                            new KvpOf("timestamp", DateTime.Now.ToString("M.d.yyyy h:mm:ss tt")),
                            new KvpOf("id", () =>
                                {
                                    var url = adBox.FindElement(By.TagName("a")).GetAttribute("href");
                                    var id = url.Substring(url.LastIndexOf("-") + "-".Length);
                                    return $"scanboat-{id}";
                                }
                            ),
                            new KvpFallback("country", () =>
                                {
                                    var infoText =
                                        adBox.FindElement(By.ClassName("item__body"))
                                            .FindElement(By.TagName("p"))
                                            .Text;

                                    var country = infoText.Substring(infoText.IndexOf("Country")).TrimEnd(new char[] { ':', ' ' }).TrimStart();
                                    return country;
                                },
                                "error"
                            ),
                            new KvpFallback("title", () =>
                                {
                                    return
                                        adBox.FindElement(By.ClassName("item__header"))
                                            .FindElement(By.ClassName("flex-1"))
                                            .FindElement(By.TagName("h2"))
                                            .Text;
                                },
                                "error"
                            ),
                            new KvpFallback("price", () =>
                                adBox.FindElement(By.ClassName("item__header"))
                                    .FindElement(By.ClassName("flex-2"))
                                    .FindElement(By.TagName("p"))
                                    .Text
                                    .Replace("EUR", "")
                                    .Replace(".", "")
                                    .Replace(",", "")
                                    .Replace("-", "")
                                    .TrimEnd(),
                                    "0"
                            )
                        );

                    foreach (var key in map.Keys)
                    {
                        map[key].ToString();
                    }
                    return map;
                }
            );
        }

        public IDictionary<string, string> Content()
        {
            return this.attributes;
        }

        public string ID()
        {
            return this.attributes["id"];
        }

        public double Price()
        {
            var price = 0.0;
            try
            {
                price = Convert.ToDouble(this.attributes["price"]);
            }
            catch (Exception ex)
            {

            }
            return price;
        }

        public string Source()
        {
            return this.attributes["source"];
        }

        public string Url()
        {
            return this.attributes["url"];
        }
    }
}
