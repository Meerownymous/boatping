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
                    new ScbPages("https://www.scanboat.com/en/boat-market/boats?SearchCriteria.BoatModelText=rassy&SearchCriteria.PriceTo=300000&SearchCriteria.PriceFrom=30000&SearchCriteria.LengthFrom=39&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.YearFrom=2015&SearchCriteria.Searched=true&SearchCriteria.ExtendedSearch=False")
                ).Value(),
                2,
                int.MaxValue
            );
        }
    }
}
