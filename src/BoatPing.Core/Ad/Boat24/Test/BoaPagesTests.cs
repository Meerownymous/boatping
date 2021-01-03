using System;
using BoatPing.Core.Ad.Boat24;
using OpenQA.Selenium.Chrome;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.Boat24
{
    public sealed class BoaPagesTests
    {
        [Fact]
        public void EnumeratesPages()
        {
            Assert.InRange(
                new LengthOf(
                    new BoaPages(new BoaDefaultSearch())
                ).Value(),
                2,
                int.MaxValue
            );
        }
    }
}
