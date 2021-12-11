using System;
using System.Collections.Generic;
using BoatPing.Core.Ad.Selenium;
using OpenQA.Selenium;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.BandOfBoats
{
    /// <summary>
    /// All ads on a BandOfBoats page.
    /// </summary>
    public class BobPageAds : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads on a BandOfBoats page.
        /// </summary>
        public BobPageAds(Uri searchPage) : base(() =>
        {
            using (var page = new BobPage(searchPage.AbsoluteUri, new ChromeHeadless()))
            {
                IList<IAd> result = new List<IAd>();
                foreach (var adBox in page.FindElements(By.ClassName("boat-card")))
                {
                    var ad = new BobAd(adBox);
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
