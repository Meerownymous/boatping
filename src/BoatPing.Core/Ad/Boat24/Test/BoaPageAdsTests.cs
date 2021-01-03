using System;
using BoatPing.Core.Ad.Boat24;
using BoatPing.Core.Page;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace BoatPing.Core.Ad.Boat24.Test
{
    public sealed class BoaPageAdsTests
    {
        [Fact]
        public void ListsAds()
        {
            Assert.NotEmpty(
                new BoaPageAds(
                    new Uri(
                        new BoaDefaultSearch().AsString()
                    )
                )
            );
        }
    }
}
