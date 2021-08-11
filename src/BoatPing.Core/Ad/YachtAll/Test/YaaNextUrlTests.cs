using System;
using BoatPing.Core.Boot24;
using Xunit;

namespace BoatPing.Core.Ad.YachtAll.Test
{
    public sealed class YaaNextUrlTests
    {
        [Fact]
        public void BuildsFromPageSegment()
        {
            Assert.Equal(
                "https://www.yachtall.com/de/segelboote/deutschland?lngf=10&lngt=14&ybf=1970&pg=3".ToLower(),
                new YaaNextUrl(new Uri("https://www.yachtall.com/de/segelboote/deutschland?lngf=10&lngt=14&ybf=1970&pg=2".ToLower()))
                    .Value()
                    .AbsoluteUri
            );
        }

        [Fact]
        public void BuildsWhenPageMissing()
        {
            Assert.Equal(
                "https://www.yachtall.com/de/segelboote/deutschland?lngf=10&lngt=14&ybf=1970&pg=2".ToLower(),
                new YaaNextUrl(new Uri("https://www.yachtall.com/de/segelboote/deutschland?lngf=10&lngt=14&ybf=1970".ToLower()))
                    .Value()
                    .AbsoluteUri
            );
        }
    }
}