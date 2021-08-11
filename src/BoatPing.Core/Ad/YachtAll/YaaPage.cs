using System;
using BoatPing.Core.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;

namespace BoatPing.Core.Ad.YachtAll
{
    /// <summary>
    /// An opened, stable YachtAll page.
    /// </summary>
    public class YaaPage : WebDriverEnvelope
    {
        /// <summary>
        /// An opened, stable Boat24 page.
        /// </summary>
        public YaaPage(IText url) : this(() => url.AsString())
        { }

        /// <summary>
        /// An opened, stable YachtAll page.
        /// </summary>
        public YaaPage(string url) : this(() => url)
        { }

        /// <summary>
        /// An opened, stable YachtAll page with cookies accepted.
        /// </summary>
        public YaaPage(Func<string> url) : this(url, new ChromeDriver())
        { }

        /// <summary>
        /// An opened, stable YachtAll page.
        /// </summary>
        public YaaPage(string url, IWebDriver origin) : this(() => url, origin)
        { }

        /// <summary>
        /// An opened, stable YachtAll page.
        /// </summary>
        public YaaPage(IText url, IWebDriver origin) : this(() => url.AsString(), origin)
        { }

        /// <summary>
        /// An opened, stable YachtAll page.
        /// </summary>
        public YaaPage(Func<string> url, IWebDriver origin) : base(() =>
            new PgOpen(url(), origin, new TimeSpan(0, 0, 10))
        )
        { }
    }
}
