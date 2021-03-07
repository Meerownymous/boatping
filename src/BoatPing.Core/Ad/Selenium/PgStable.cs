using System;
using OpenQA.Selenium;

namespace BoatPing.Core.Page
{
    /// <summary>
    /// A page that is awaited to be stable (no js execution ongoing)
    /// </summary>
    public class PgStable : WebDriverEnvelope
    {
        /// <summary>
        /// A page that is awaited to be stable (no js execution ongoing)
        /// </summary>
        public PgStable(IWebDriver origin) : this(origin, new TimeSpan(0, 0, 30))
        { }

        /// <summary>
        /// A page that is awaited to be stable (no js execution ongoing)
        /// </summary>
        public PgStable(IWebDriver origin, TimeSpan timeout) : base(() =>
        {
            var start = DateTime.Now;
            var lastState = "";
            while (lastState != origin.PageSource)
            {
                System.Threading.Thread.Sleep(1000);
                if (lastState == origin.PageSource)
                {
                    break;
                }
                lastState = origin.PageSource;
                if (DateTime.Now > start + timeout)
                {
                    break;
                }
            }

            return origin;
        })
        { }
    }
}
