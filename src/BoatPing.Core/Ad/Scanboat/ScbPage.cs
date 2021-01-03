﻿using System;
using BoatPing.Core.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;

namespace BoatPing.Core.Ad.Scanboat
{
    /// <summary>
    /// An opened, stable scanboat page.
    /// </summary>
    public class ScbPage : WebDriverEnvelope
    {
        /// <summary>
        /// An opened, stable scanboat.com page.
        /// </summary>
        public ScbPage(IText url) : this(() => url.AsString())
        { }

        /// <summary>
        /// An opened, stable scanboat.com page.
        /// </summary>
        public ScbPage(string url) : this(() => url)
        { }

        /// <summary>
        /// An opened, stable scanboat.com page.
        /// </summary>
        public ScbPage(Func<string> url) : this(url, new ChromeDriver())
        { }

        /// <summary>
        /// An opened, stable scanboat.com page.
        /// </summary>
        public ScbPage(string url, IWebDriver origin) : this(() => url, origin)
        { }

        /// <summary>
        /// An opened, stable scanboat.com page.
        /// </summary>
        public ScbPage(IText url, IWebDriver origin) : this(() => url.AsString(), origin)
        { }

        /// <summary>
        /// An opened, stable scanboat.com page.
        /// </summary>
        public ScbPage(Func<string> url, IWebDriver origin) : base(() =>
            new PgOpen(url(), origin, new TimeSpan(0,0,3))
        )
        { }
    }
}