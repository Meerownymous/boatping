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
            using (var page =
                new PgOpen(
                    new B24DefaultSearch()
                )
            )
            {
                Assert.NotEmpty(new B24PageAds(page));
            }
        }
    }
}
