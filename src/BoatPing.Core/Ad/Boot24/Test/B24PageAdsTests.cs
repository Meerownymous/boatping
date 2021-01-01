using System;
using BoatPing.Core.Page;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace BoatPing.Core.Boot24
{
    public sealed class B24PageAdsTests
    {
        [Fact]
        public void ListsAds()
        {
            Assert.NotEmpty(
                new B24PageAds(
                    new Uri(
                        new B24DefaultSearch().AsString()
                    )
                )
            );
        }
    }
}
