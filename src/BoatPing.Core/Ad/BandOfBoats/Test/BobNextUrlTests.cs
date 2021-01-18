using System;
using BoatPing.Core.Boot24;
using Xunit;

namespace BoatPing.Core.Ad.BandOfBoats.Test
{
    public sealed class BobNextUrlTests
    {
        [Fact]
        public void BuildsFromPageSegment()
        {
            Assert.Equal(
                "https://www.bandofboats.com/de/boot-kaufen?ref_category%5B%5D=9&country%5B%5D=DE&page=1&price_min=20000&price_max=100000&year_min=1970&year_max=&loa_min=10&loa_max=&beam_min=&beam_max=&horse_power_min=&horse_power_max=",
                new BobNextUrl(new Uri("https://www.bandofboats.com/de/boot-kaufen?ref_category%5B%5D=9&country%5B%5D=DE&page=2&price_min=20000&price_max=100000&year_min=1970&year_max=&loa_min=10&loa_max=&beam_min=&beam_max=&horse_power_min=&horse_power_max="))
                    .Value()
                    .AbsoluteUri
            );
        }
    }
}