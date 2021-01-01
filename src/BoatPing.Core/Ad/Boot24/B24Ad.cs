using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Ad.Boot24
{
    /// <summary>
    /// A boot24.de ad scraped directly from the search page.
    /// </summary>
    public class B24Ad : IAd
    {
        private readonly MapOf attributes;

        /// <summary>
        /// A boot24.de ad scraped directly from the search page.
        /// </summary>
        public B24Ad(IWebElement adBox)
        {
            this.attributes =
                new MapOf(
                    new KvpOf("source", "boot24.de"),
                    new KvpOf("url", adBox.GetAttribute("href")),
                    new KvpOf("timestamp", DateTime.Now.ToString("M.d.yyyy h:mm:ss tt")),
                    new KvpFallback("id", () => $"boot24-{adBox.GetAttribute("data-objekt-nr")}", "error"),
                    new KvpFallback("country", () =>
                        {

                            return
                                new LastOf<IWebElement>(
                                    adBox.FindElements(By.ClassName("details_left"))
                                )
                                .Value()
                                .Text;
                        },
                        "error"
                    ),
                    new KvpFallback("title", () => adBox.FindElement(By.ClassName("sr-objektbox-us")).Text, "error"),
                    new KvpFallback("price", () => adBox.FindElement(By.ClassName("sr-price")).Text.Replace(".", "").Replace("€", "").TrimEnd(), "0")
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
