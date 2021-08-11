using System;
using BoatPing.Core.Ad.Boat24;
using OpenQA.Selenium.Chrome;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.YachtAll.Test
{
    public sealed class YaaPagesTests
    {
        [Fact]
        public void EnumeratesPages()
        {
            Assert.InRange(
                new LengthOf(
                    new YaaPages("https://www.yachtall.com/de/segelboote?lngf=10&lngt=14&ybf=1970")
                ).Value(),
                2,
                int.MaxValue
            );
        }
    }
}
