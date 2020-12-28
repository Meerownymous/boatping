using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.PortableExecutable;
using BoatPing.Core.Model;
using BriX;
using OpenQA.Selenium;
using Yaapii.Atoms;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Boot24.Datum
{
    /// <summary>
    /// A boot24 ad.
    /// </summary>
    public class B24Ad : IAd
    {
        private readonly MapOf attributes;

        /// <summary>
        /// A boot24 ad.
        /// </summary>
        public B24Ad(string url)
        {
            this.attributes =
                new MapOf(() =>
                {
                    using (var page = new B24Page(url))
                    {
                        var map =
                        new Yaapii.Atoms.Map.Joined(
                            new MapOf(
                                new KvpFallback("id", () => $"boot24:{page.FindElement(By.Id("onr")).Text}", "error"),
                                new KvpFallback("country", () =>
                                    {
                                        return
                                            new LastOf<IWebElement>(
                                                page.FindElements(By.PartialLinkText("https://www.boot24.com/bootstandorte"))
                                            )
                                            .Value()
                                            .Text;
                                    },
                                    "error"
                                ),
                                new KvpFallback("title", () => page.FindElement(By.ClassName("detail-img-greybox")).FindElement(By.TagName("h1")).Text, "error"),
                                new KvpFallback("price", () => page.FindElement(By.Id("preis")).Text.Replace(".", ""), "error")
                            ),
                            new MapOf(
                                new Yaapii.Atoms.Enumerable.Mapped<IWebElement, IKvp>(
                                    elem =>
                                    {
                                        var key = "error";
                                        var value = "error";
                                        try
                                        {
                                            key = elem.FindElement(By.TagName("div")).Text.TrimEnd(':');
                                            if (elem.FindElements(By.TagName("a")).Count > 0)
                                            {
                                                value = elem.FindElement(By.TagName("a")).Text;
                                            }
                                            else
                                            {
                                                value = elem.FindElement(By.TagName("span")).Text;
                                            }
                                        }
                                        catch (Exception) { }
                                        return new KvpOf(key, value);
                                    },
                                    page.FindElements(By.XPath("//ul[@class='detail-liste']/li"))
                                )
                            )
                        );
                        map.Keys.GetEnumerator();
                        return map;
                    }
                });
        }

        public string ID()
        {
            return this.attributes["id"];
        }

        public string Title()
        {
            return this.attributes["title"];
        }

        public double Price()
        {
            return Convert.ToDouble(this.attributes["price"], CultureInfo.InvariantCulture);
        }

        public IEnumerable<string> Pix()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string,string> Content()
        {
            return this.attributes;
        }
    }
}
