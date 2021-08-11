using System;
using System.Collections.Generic;
using BoatPing.Core.Ad.Selenium;
using OpenQA.Selenium;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.YachtAll
{
    /// <summary>
    /// All ads on a YachtAll page.
    /// </summary>
    public class YaaPageAds : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads on a YachtAll page.
        /// </summary>
        public YaaPageAds(Uri searchPage) : base(() =>
        {
            using (var page = new YaaPage(searchPage.AbsoluteUri, new ChromeHeadless()))
            {
                IList<IAd> result = new List<IAd>();
                foreach (var adBox in page.FindElements(By.ClassName("boatlist-subbox")))
                {
                    var ad = new YaaAd(adBox);
                    ad.ID(); //trigger ad building while page is open
                    result.Add(ad);
                }
                return result;
            }
        },
            false
        )
        { }
    }
}
