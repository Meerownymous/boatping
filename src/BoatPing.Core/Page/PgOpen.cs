using System;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;

namespace BoatPing.Core.Page
{
    /// <summary>
    /// A page opened at the given url.
    /// </summary>
    public class PgOpen : WebDriverEnvelope
    {
        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(IText url) : this(() => new Uri(url.AsString()))
        { }

        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(string url) : this(() => new Uri(url))
        { }

        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(Uri url) : this(() => url)
        { }

        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(Func<Uri> url) : base(() =>
        {
            var page = new ChromeDriver();
            page.Navigate().GoToUrl(url());
            return new PgStable(page, new TimeSpan(0, 0, 30));
        })
        { }
    }
}
