using System;
using BoatPing.Core.Ad.Boat24;
using OpenQA.Selenium.Chrome;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.BandOfBoats.Test
{
    public sealed class BobPagesTests
    {
        [Fact]
        public void EnumeratesPages()
        {
            var pages = new BobPages("https://www.bandofboats.com/de/boot-kaufen?ref_category%5B%5D=9&price_min=20000&price_max=100000&year_min=1970&year_max=&loa_min=10&loa_max=&beam_min=&beam_max=&horse_power_min=&horse_power_max=").GetEnumerator();

            Assert.True(
                pages.MoveNext()
            );
        }
    }
}
