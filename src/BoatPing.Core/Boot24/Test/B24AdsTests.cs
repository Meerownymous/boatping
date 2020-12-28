using System;
using System.Collections.Generic;
using Xunit;

namespace BoatPing.Core.Boot24
{
    public sealed class B24PagesAdsTests
    {
        [Fact]
        public void EnumeratesAds()
        {
            var prices = new List<string>();
            foreach(var ad in new B24Ads(new B24DefaultSearch()))
            {
                prices.Add(ad.Price().ToString());
            }
        }
    }
}
