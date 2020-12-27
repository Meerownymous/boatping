using System;
using BoatPing.Core.Page;
using OpenQA.Selenium;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// A boot24 page where the cookie window has been accepted.
    /// </summary>
    public class B24CookieAccepted : WebDriverEnvelope
    {
        /// <summary>
        /// A boot24 page where the cookie window has been accepted.
        /// </summary>
        public B24CookieAccepted(IWebDriver origin) : base(() =>
        {
            foreach(var cookieBtn in origin.FindElements(By.Id("onetrust-accept-btn-handler")))
            {
                cookieBtn.Click();
            }
            return new PgStable(origin);
        })
        { }
    }
}
