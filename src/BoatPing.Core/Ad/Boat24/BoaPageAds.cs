using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoatPing.Core.Ad.Boot24;
using BoatPing.Core.Ad.Selenium;
using BoatPing.Core.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.Boat24
{
    /// <summary>
    /// All ads on a Boat24 page.
    /// </summary>
    public class BoaPageAds : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads on a Boat24 page.
        /// </summary>
        public BoaPageAds(Uri searchPage) : base(() =>
        {
            using (var page = new BoaPage(searchPage.AbsoluteUri, new ChromeHeadless()))
            {
                IList<IAd> result = new List<IAd>();
                foreach (var adBox in page.FindElements(By.ClassName("blurb--strip")))
                {
                    var ad = new BoaAd(adBox);
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
