using System;
using OpenQA.Selenium;

namespace BoatPing.Core.Page
{
    /// <summary>
    /// A page that is awaited to be stable (no js execution ongoing)
    /// </summary>
    public class PgStable : WebDriverEnvelope
    {
        public PgStable(IWebDriver origin, TimeSpan timeout) : base(() =>
        {
            var start = DateTime.Now;
            var lastState = origin.PageSource;
            while (lastState != origin.PageSource)
            {
                if (lastState == origin.PageSource)
                {
                    break;
                }
                lastState = origin.PageSource;
                System.Threading.Thread.Sleep(500);
            }
            if (DateTime.Now > start + timeout)
            {
                throw new ApplicationException($"page_unstable_for_timeout:{timeout.TotalSeconds}s");
            }
            return origin;
        })
        { }
    }
}
