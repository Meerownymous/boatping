using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Ad.BandOfBoats
{
    /// <summary>
    /// A BandOfBoats ad scraped directly from the search page.
    /// </summary>
    public sealed class BobAd : IAd
    {
        private readonly MapOf attributes;

        /// <summary>
        /// A BandOfBoats ad scraped directly from the search page.
        /// </summary>
        public BobAd(IWebElement adBox)
        {
            this.attributes =
                new MapOf(() =>
                {
                    var map =
                        new MapOf(
                            new KvpOf("source", "band-of-boats.com"),
                            new KvpOf("url", adBox.FindElement(By.ClassName("card-link")).GetAttribute("href")),
                            new KvpOf("timestamp", DateTime.Now.ToString("M.d.yyyy h:mm:ss tt")),
                            new KvpFallback("id", () =>
                                {
                                    var url = adBox.FindElement(By.ClassName("card-link")).GetAttribute("href");
                                    var id = url.Substring(url.LastIndexOf("/")).TrimEnd('/');
                                    return $"band-of-boats-{id}";
                                },
                                "error"
                            ),
                            new KvpFallback("country", () =>
                                {
                                    var country =
                                        new LastOf<IWebElement>(
                                            adBox.FindElement(By.ClassName("card-body"))
                                                .FindElement(By.ClassName("pb-1"))
                                                .FindElements(By.ClassName("text-steelblue"))
                                        )
                                        .Value()
                                        .Text;
                                    return country;
                                },
                                "error"
                            ),
                            new KvpFallback("title", () => adBox.FindElement(By.ClassName("card-title")).Text, "error"),
                            new KvpFallback("price", () =>
                                {
                                    var text =
                                        new LastOf<IWebElement>(
                                            adBox.FindElement(By.ClassName("card-body"))
                                                .FindElements(By.ClassName("text-secondaire"))
                                        )
                                        .Value()
                                        .Text
                                        .Replace("EUR", "")
                                        .Replace(".", "")
                                        .Replace(",", "")
                                        .Replace("-", "")
                                        .Replace("&nbsp;", "")
                                        .TrimEnd();
                                    return text.Substring(0, text.IndexOf("€"));
                                },
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
