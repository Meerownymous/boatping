using System;
using OpenQA.Selenium;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// All ads on a Boot24 page.
    /// </summary>
    public class B24PageAds : ManyEnvelope<string>
    {
        /// <summary>
        /// All ads on a Boot24 page.
        /// </summary>
        public B24PageAds(IWebDriver page) : base(() =>
            new Mapped<IWebElement, string>(
                elem => elem.GetAttribute("href"),
                page.FindElements(By.ClassName("sr-objektbox-in"))
            ),
            false
        )
        { }
    }
}
