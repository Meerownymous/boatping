using System;
using Xunit;

namespace BoatPing.Core.Ad.Scanboat.Test
{
    public sealed class ScbPageUrlTests
    {
        [Fact]
        public void BuildsUrl()
        {
            Assert.Equal(
                "https://www.scanboat.com/en/boats?page=2&SearchCriteria.BoatModelText=&SearchCriteria.BoatTypeID=1&SearchCriteria.YearIntervalId=110&SearchCriteria.HullTypeID=0&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.EnginePlacementID=0&SearchCriteria.FuelTypeID=0&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.WidthIntervalId=0&SearchCriteria.DaysOld=0&SearchCriteria.FreeText=&SearchCriteria.Searched=true&SearchCriteria.ExtendedSearch=True&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8".ToLower(),
                new ScbPageUrl(
                    new Uri("https://www.scanboat.com/en/boats?page=1&SearchCriteria.BoatModelText=&SearchCriteria.BoatTypeID=1&SearchCriteria.YearIntervalId=110&SearchCriteria.HullTypeID=0&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.EnginePlacementID=0&SearchCriteria.FuelTypeID=0&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.WidthIntervalId=0&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.DaysOld=0&SearchCriteria.FreeText=&SearchCriteria.Searched=true&SearchCriteria.ExtendedSearch=True".ToLower()),
                    2
                ).Value()
                .AbsoluteUri
                .ToLower()
            );
        }

        [Fact]
        public void BuildsUrlWhenPageMissing()
        {
            Assert.Equal(
                "https://www.scanboat.com/en/boats?SearchCriteria.BoatModelText=&SearchCriteria.BoatTypeID=1&SearchCriteria.YearIntervalId=110&SearchCriteria.HullTypeID=0&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.EnginePlacementID=0&SearchCriteria.FuelTypeID=0&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.WidthIntervalId=0&SearchCriteria.DaysOld=0&SearchCriteria.FreeText=&SearchCriteria.Searched=true&SearchCriteria.ExtendedSearch=True&page=2&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8".ToLower(),
                new ScbPageUrl(
                    new Uri("https://www.scanboat.com/en/boats?SearchCriteria.BoatModelText=&SearchCriteria.BoatTypeID=1&SearchCriteria.YearIntervalId=110&SearchCriteria.HullTypeID=0&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.EnginePlacementID=0&SearchCriteria.FuelTypeID=0&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.WidthIntervalId=0&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.DaysOld=0&SearchCriteria.FreeText=&SearchCriteria.Searched=true&SearchCriteria.ExtendedSearch=True".ToLower()),
                    2
                ).Value()
                .AbsoluteUri
                .ToLower()
            );
        }
    }
}
