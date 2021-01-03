using System;
using BoatPing.Core.Ad.Boat24;
using BoatPing.Core.Page;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace BoatPing.Core.Ad.Scanboat.Test
{
    public sealed class ScbPageAdsTests
    {
        [Fact]
        public void ListsAds()
        {
            Assert.NotEmpty(
                new ScbPageAds(
                    new Uri(
                        new ScbSearch19701985().AsString()
                    )
                )
            );
        }
    }
}
