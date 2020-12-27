using System;
using OpenQA.Selenium;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// Checks if a Boot24 search result page has a followup page.
    /// </summary>
    public class B24HasNext : AssumptionEnvelope
    {
        /// <summary>
        /// Checks if a Boot24 search result page has a followup page.
        /// </summary>
        public B24HasNext(IWebDriver page) : base(() =>
            page.FindElements(By.ClassName("sitenumber-navi-hide")).Count > 0
        )
        { }
    }
}
