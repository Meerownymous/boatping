using System;
using OpenQA.Selenium;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core.Page
{
    /// <summary>
    /// A page on which the first element which matches the given query has been clicked.
    /// </summary>
    public class PgClickedOn : WebDriverEnvelope
    {
        /// <summary>
        /// A page on which the first element which matches the given query has been clicked.
        /// </summary>
        public PgClickedOn(By elementQuery, IWebDriver page) : base(() =>
        {
            new FirstOf<IWebElement>(
                page.FindElements(elementQuery),
                new InvalidOperationException($"cannot_click_notfound_element:{elementQuery.ToString()}")
            )
            .Value()
            .Click();
            return new PgStable(page);
        })
        {
        }
    }
}
