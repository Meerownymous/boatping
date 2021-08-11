using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OpenQA.Selenium;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Ad.YachtAll
{
    /// <summary>
    /// A YachtAll.com ad scraped directly from the search page.
    /// </summary>
    public class YaaAd : IAd
    {
        private readonly MapOf attributes;

        /// <summary>
        /// A YachtAll.com ad scraped directly from the search page.
        /// </summary>
        public YaaAd(IWebElement adBox)
        {
            this.attributes =
                new MapOf(() =>
                {
                    var map =
                        new MapOf(
                            new KvpOf("source", "yachtall.com"),
                            new KvpOf("url", adBox.FindElement(By.ClassName("js-hrefBoat")).GetAttribute("href")),
                            new KvpOf("timestamp", DateTime.Now.ToString("M.d.yyyy h:mm:ss tt")),
                            new KvpFallback("id", () =>
                                {
                                    var url = adBox.FindElement(By.ClassName("js-hrefBoat")).GetAttribute("href");
                                    var id = url.Substring(url.LastIndexOf("-") + 1);
                                    return $"yachtall-{id}";
                                },
                                "error"
                            ),
                            new KvpFallback("country", () =>
                                {
                                    var elements = adBox.FindElements(By.TagName("span"));
                                    bool catchNext = false;
                                    var result = "unknown";
                                    foreach(var elem in elements)
                                    {
                                        if (catchNext)
                                        {
                                            result = elem.Text;
                                            break;
                                        }

                                        if (elem.Text.Contains("Liegeplatz"))
                                        {
                                            catchNext = true;
                                        }
                                        
                                    }
                                    return result;
                                },
                                "error"
                            ),
                            new KvpFallback("title", () =>
                                {
                                    var link = adBox.FindElement(By.TagName("a")).GetAttribute("href");
                                    var tail = link.Substring(link.LastIndexOf("/") + 1);
                                    var title = tail.Substring(0, tail.LastIndexOf("-")).Trim('-');
                                    return title;
                                },
                                "error"
                            ),
                            new KvpFallback("price", () =>
                                {
                                    var text = adBox.FindElement(By.ClassName("color-orange-bold")).Text;
                                    var numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                                    var price = String.Empty;
                                    foreach(char c in text)
                                    {
                                        if (numbers.Contains(c))
                                        {
                                            price += c;
                                        }
                                    };
                                    return price;                                    
                                },
                                "error"
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
