using System;
using System.Collections.Generic;
using Xunit;

namespace BoatPing.Core.Boot24.Test
{
    public sealed class B24PagesAdsTests
    {
        [Fact]
        public void EnumeratesAds()
        {
            Assert.True(
                new B24Ads(
                    new B24DefaultSearch(),
                    0,
                    int.MaxValue
                )
                .GetEnumerator()
                .MoveNext()
            );
        }
    }
}
