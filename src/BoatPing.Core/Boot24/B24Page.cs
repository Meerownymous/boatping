using System;
using BoatPing.Core.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// An opened, stable Boot24 page with cookies accepted.
    /// </summary>
    public class B24Page : WebDriverEnvelope
    {
        /// <summary>
        /// An opened, stable Boot24 page with cookies accepted.
        /// </summary>
        public B24Page(IText url) : this(() => url.AsString())
        { }

        /// <summary>
        /// An opened, stable Boot24 page with cookies accepted.
        /// </summary>
        public B24Page(string url) : this(() => url)
        { }

        /// <summary>
        /// An opened, stable Boot24 page with cookies accepted.
        /// </summary>
        public B24Page(Func<string> url) : this(url, new ChromeDriver())
        { }

        /// <summary>
        /// An opened, stable Boot24 page with cookies accepted.
        /// </summary>
        public B24Page(string url, IWebDriver origin) : this(() => url, origin)
        { }

        /// <summary>
        /// An opened, stable Boot24 page with cookies accepted.
        /// </summary>
        public B24Page(IText url, IWebDriver origin) : this(() => url.AsString(), origin)
        { }

        /// <summary>
        /// An opened, stable Boot24 page with cookies accepted.
        /// </summary>
        public B24Page(Func<string> url, IWebDriver origin) : base(() =>
            new B24CookieAccepted(
                new PgOpen(url(), origin)
            )
        )
        { }
    }
}
