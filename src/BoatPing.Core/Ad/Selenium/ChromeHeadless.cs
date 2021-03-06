using System;
using BoatPing.Core.Page;
using OpenQA.Selenium.Chrome;

namespace BoatPing.Core.Ad.Selenium
{
    /// <summary>
    /// A chrome driver which is headless.
    /// </summary>
    public class ChromeHeadless : WebDriverEnvelope
    {
        /// <summary>
        /// A chrome driver which is headless.
        /// </summary>
        public ChromeHeadless() : base(() =>
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            options.AddArgument("window-size=1024,768");
            //options.AddArgument("--headless");
            return new ChromeDriver(options);
        })
        { }
    }
}
