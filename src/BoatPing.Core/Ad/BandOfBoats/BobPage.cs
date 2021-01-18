using System;
using BoatPing.Core.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;

namespace BoatPing.Core.Ad.BandOfBoats
{
    /// <summary>
    /// An opened, stable BandOfBoats page.
    /// </summary>
    public class BobPage : WebDriverEnvelope
    {
        /// <summary>
        /// An opened, stable BandOfBoats page.
        /// </summary>
        public BobPage(IText url) : this(() => url.AsString())
        { }

        /// <summary>
        /// An opened, stable BandOfBoats page.
        /// </summary>
        public BobPage(string url) : this(() => url)
        { }

        /// <summary>
        /// An opened, stable BandOfBoats page.
        /// </summary>
        public BobPage(Func<string> url) : this(url, new ChromeDriver())
        { }

        /// <summary>
        /// An opened, stable BandOfBoats page.
        /// </summary>
        public BobPage(string url, IWebDriver origin) : this(() => url, origin)
        { }

        /// <summary>
        /// An opened, stable BandOfBoats page.
        /// </summary>
        public BobPage(IText url, IWebDriver origin) : this(() => url.AsString(), origin)
        { }

        /// <summary>
        /// An opened, stable BandOfBoats page.
        /// </summary>
        public BobPage(Func<string> url, IWebDriver origin) : base(() =>
            new PgOpen(url(), origin, new TimeSpan(0, 0, 10))
        )
        { }
    }
}
