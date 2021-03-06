﻿using System;
using BoatPing.Core.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;

namespace BoatPing.Core.Ad.Boat24
{
    /// <summary>
    /// An opened, stable Boat24 page.
    /// </summary>
    public class BoaPage : WebDriverEnvelope
    {
        /// <summary>
        /// An opened, stable Boat24 page.
        /// </summary>
        public BoaPage(IText url) : this(() => url.AsString())
        { }

        /// <summary>
        /// An opened, stable Boat24 page.
        /// </summary>
        public BoaPage(string url) : this(() => url)
        { }

        /// <summary>
        /// An opened, stable Boat24 page with cookies accepted.
        /// </summary>
        public BoaPage(Func<string> url) : this(url, new ChromeDriver())
        { }

        /// <summary>
        /// An opened, stable Boat24 page.
        /// </summary>
        public BoaPage(string url, IWebDriver origin) : this(() => url, origin)
        { }

        /// <summary>
        /// An opened, stable Boat24 page.
        /// </summary>
        public BoaPage(IText url, IWebDriver origin) : this(() => url.AsString(), origin)
        { }

        /// <summary>
        /// An opened, stable Boat24 page.
        /// </summary>
        public BoaPage(Func<string> url, IWebDriver origin) : base(() =>
            new PgOpen(url(), origin, new TimeSpan(0, 0, 10))
        )
        { }
    }
}
