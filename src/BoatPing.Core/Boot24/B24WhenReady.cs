using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Yaapii.Atoms;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// Execute an action after given page is ready.
    /// </summary>
    public class B24WaitForStable : IAction
    {
        private readonly IWebDriver page;
        private readonly TimeSpan timeout;

        /// <summary>
        /// Execute an action after given page is ready.
        /// </summary>
        public B24WaitForStable(IWebDriver page, TimeSpan timeout)
        {
            this.page = page;
            this.timeout = timeout;
        }

        public void Invoke()
        {
            var start = DateTime.Now;
            var lastState = this.page.PageSource;
            while (lastState != this.page.PageSource)
            {
                if (lastState == this.page.PageSource)
                {
                    break;
                }
                lastState = this.page.PageSource;
                System.Threading.Thread.Sleep(1000);
            }
            if(DateTime.Now > start + this.timeout)
            {
                throw new ApplicationException($"page_unstable_for_timeout:{this.timeout.TotalSeconds}s");
            }
        }
    }
}
