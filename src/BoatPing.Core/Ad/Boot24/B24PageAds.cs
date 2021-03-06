using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoatPing.Core.Ad.Boot24;
using BoatPing.Core.Ad.Selenium;
using BoatPing.Core.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// All ads on a Boot24 page.
    /// </summary>
    public class B24PageAds : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads on a Boot24 page.
        /// </summary>
        public B24PageAds(Uri searchPage) : base(() =>
            {
                using (var page = new B24Page(searchPage.AbsoluteUri, new ChromeHeadless()))
                {
                    IList<IAd> result = new List<IAd>();
                    foreach(var adBox in page.FindElements(By.ClassName("sr-objektbox-in")))
                    {
                        var ad = new B24Ad(adBox);
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
