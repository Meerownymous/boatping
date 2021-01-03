using System;
using BoatPing.Core.Ad.Boat24;
using OpenQA.Selenium.Chrome;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.Scanboat.Test
{
    public sealed class ScbPagesTests
    {
        [Fact]
        public void EnumeratesPages()
        {
            Assert.InRange(
                new LengthOf(
                    new ScbPages(new ScbSearch19701985())
                ).Value(),
                2,
                int.MaxValue
            );
        }
    }
}
