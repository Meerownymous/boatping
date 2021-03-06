using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoatPing.Core.Ad.Boot24;
using BoatPing.Core.Ad.Selenium;
using BoatPing.Core.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.Scanboat
{
    /// <summary>
    /// All ads on a scanboat page.
    /// </summary>
    public class ScbPageAds : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads on a scanboat page.
        /// </summary>
        public ScbPageAds(Uri searchPage) : base(() =>
            {
                using (var page = new ScbPage(searchPage.AbsoluteUri, new ChromeHeadless()))
                {
                    IList<IAd> result = new List<IAd>();
                    foreach (var adBox in
                        page.FindElement(By.ClassName("boat-list__body"))
                            .FindElements(By.ClassName("item")))
                    {
                        var ad = new ScbAd(adBox);
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
