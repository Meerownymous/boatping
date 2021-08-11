using System;
using BoatPing.Core.Ad.Boat24;
using BoatPing.Core.Page;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace BoatPing.Core.Ad.YachtAll.Test
{
    public sealed class YaaPageAdsTests
    {
        [Fact]
        public void ListsAds()
        {
            Assert.NotEmpty(
                new YaaPageAds(
                    new Uri(
                        "https://www.yachtall.com/de/segelboote/deutschland?lngf=10&lngt=14&ybf=1970"
                    )
                )
            );
        }
    }
}
