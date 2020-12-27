using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core.Page
{
    /// <summary>
    /// Envelope for webdrivers.
    /// </summary>
    public abstract class WebDriverEnvelope : IWebDriver
    {
        private readonly IScalar<IWebDriver> webDriver;

        /// <summary>
        /// Envelope for webdrivers.
        /// </summary>
        public WebDriverEnvelope(Func<IWebDriver> webDriver)
        {
            this.webDriver = new ScalarOf<IWebDriver>(webDriver);
        }

        public string Url
        {
            get => this.webDriver.Value().Url; set => this.webDriver.Value().Url = value;
        }


        public string Title => this.webDriver.Value().Title;

        public string PageSource => this.webDriver.Value().PageSource;

        public string CurrentWindowHandle => this.webDriver.Value().CurrentWindowHandle;

        public ReadOnlyCollection<string> WindowHandles => this.webDriver.Value().WindowHandles;

        public void Close()
        {
            this.webDriver.Value().Close();
        }

        public void Dispose()
        {
            this.webDriver.Value().Dispose();
        }

        public IWebElement FindElement(By by)
        {
            return this.webDriver.Value().FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return this.webDriver.Value().FindElements(by);
        }

        public IOptions Manage()
        {
            return this.webDriver.Value().Manage();
        }

        public INavigation Navigate()
        {
            return this.webDriver.Value().Navigate();
        }

        public void Quit()
        {
            this.webDriver.Value().Quit();
        }

        public ITargetLocator SwitchTo()
        {
            return this.webDriver.Value().SwitchTo();
        }
    }
}
