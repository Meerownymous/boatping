using System;
using BoatPing.Core.Page;
using OpenQA.Selenium;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// Search reult page by calling the next button's data-url.
    /// </summary>
    public class B24NextPage : WebDriverEnvelope
    {
        /// <summary>
        /// Search reult page by calling the next-button's data-url.
        /// </summary>
        public B24NextPage(IWebDriver origin) : base(() =>
        {
            var nextUrl = origin.FindElement(By.XPath("//span[@title='nächste']")).GetAttribute("data-url");
            return new B24Page(nextUrl, origin);
        })
        { }
    }
}
