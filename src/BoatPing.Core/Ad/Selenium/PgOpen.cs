using System;
using OpenQA.Selenium;
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
        public PgOpen(IText url, TimeSpan maxWait) : this(() => new Uri(url.AsString()), new ChromeDriver(), maxWait)
        { }

        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(string url, TimeSpan maxWait) : this(() => new Uri(url), new ChromeDriver(), maxWait)
        { }

        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(Uri url, TimeSpan maxWait) : this(() => url, new ChromeDriver(), maxWait)
        { }

        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(IText url, IWebDriver page, TimeSpan maxWait) : this(() => new Uri(url.AsString()), page, maxWait)
        { }

        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(string url, IWebDriver page, TimeSpan maxWait) : this(() => new Uri(url), page, maxWait)
        { }

        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(Uri url, IWebDriver page, TimeSpan maxWait) : this(() => url, page, maxWait)
        { }

        /// <summary>
        /// A page opened at the given url.
        /// </summary>
        public PgOpen(Func<Uri> url, IWebDriver page, TimeSpan maxWait) : base(() =>
            {
                var retries = 5;
                var success = false;
                for (var tryCount = 0; tryCount < retries; tryCount++)
                {
                    try
                    {
                        page.Manage().Timeouts().PageLoad = new TimeSpan(0, 3, 0);
                        page.Navigate().GoToUrl(url());
                        success = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                if(!success)
                {
                    throw new ApplicationException($"page_load_timeout:{url().AbsoluteUri.ToString()}");
                }

                return new PgStable(page, maxWait);
            })
        { }
    }
}
