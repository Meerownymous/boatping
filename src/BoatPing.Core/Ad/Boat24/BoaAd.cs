using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Ad.Boat24
{
    /// <summary>
    /// A boat24.com ad scraped directly from the search page.
    /// </summary>
    public class BoaAd : IAd
    {
        private readonly MapOf attributes;

        /// <summary>
        /// A boat24.com ad scraped directly from the search page.
        /// </summary>
        public BoaAd(IWebElement adBox)
        {
            this.attributes =
                new MapOf(() =>
                {
                    var map =
                        new MapOf(
                            new KvpOf("source", "boat24.com"),
                            new KvpOf("url", adBox.GetAttribute("data-link")),
                            new KvpOf("timestamp", DateTime.Now.ToString("M.d.yyyy h:mm:ss tt")),
                            new KvpFallback("id", () =>
                                {
                                    var url = adBox.GetAttribute("data-link");
                                    var id = url.Substring(url.IndexOf("/detail/")).Replace("/detail/", "").TrimEnd('/');
                                    return $"boat24-{id}";
                                },
                                "error"
                            ),
                            new KvpFallback("country", () =>
                                {
                                    var country =
                                        new LastOf<IWebElement>(
                                            adBox.FindElements(By.ClassName("blurb__location"))
                                        )
                                        .Value()
                                        .Text;
                                    return country;
                                },
                                "error"
                            ),
                            new KvpFallback("title", () => adBox.GetAttribute("title"), "error"),
                            new KvpFallback("price", () =>
                                adBox.FindElement(By.ClassName("blurb__price"))
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
