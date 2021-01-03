using System;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.Scanboat
{
    /// <summary>
    /// search on scanboat.com in years 1970 - 1985
    /// </summary>
    public class ScbSearch19701985 : TextEnvelope
    {
        /// <summary>
        /// search on scanboat.com in years 1970 - 1985
        /// </summary>
        public ScbSearch19701985() : base(() =>
            "https://www.scanboat.com/en/boats?page=1&SearchCriteria.BoatTypeID=1&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.PriceFrom=30000.00&SearchCriteria.PriceTo=85000.00&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.LengthFrom=33.00&SearchCriteria.LengthTo=40.00&SearchCriteria.YearIntervalId=110&SearchCriteria.YearFrom=1970.00&SearchCriteria.YearTo=1985.00&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.ExtendedSearch=True&SearchCriteria.Searched=True",
            false
        )
        { }
    }

    /// <summary>
    /// search on scanboat.com in years 1985 - 1995
    /// </summary>
    public class ScbSearch19851995 : TextEnvelope
    {
        /// <summary>
        /// search on scanboat.com in years 1985 - 1995
        /// </summary>
        public ScbSearch19851995() : base(() =>
            "https://www.scanboat.com/en/boats?page=1&SearchCriteria.BoatTypeID=1&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.PriceFrom=30000.00&SearchCriteria.PriceTo=85000.00&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.LengthFrom=33.00&SearchCriteria.LengthTo=40.00&SearchCriteria.YearIntervalId=111&SearchCriteria.YearFrom=1985.00&SearchCriteria.YearTo=1995.00&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.ExtendedSearch=True&SearchCriteria.Searched=True",
            false
        )
        { }
    }

    /// <summary>
    /// search on scanboat.com in years 1995 - 2000
    /// </summary>
    public class ScbSearch19952000 : TextEnvelope
    {
        /// <summary>
        /// search on scanboat.com in years 1995 - 2000
        /// </summary>
        public ScbSearch19952000() : base(() =>
            "https://www.scanboat.com/en/boats?page=1&SearchCriteria.BoatTypeID=1&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.PriceFrom=30000.00&SearchCriteria.PriceTo=85000.00&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.LengthFrom=33.00&SearchCriteria.LengthTo=40.00&SearchCriteria.YearIntervalId=112&SearchCriteria.YearFrom=1995.00&SearchCriteria.YearTo=2000.00&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.ExtendedSearch=True&SearchCriteria.Searched=True",
            false
        )
        { }
    }

    /// <summary>
    /// search on scanboat.com in years 2000 - 2015
    /// </summary>
    public class ScbSearch20002015 : TextEnvelope
    {
        /// <summary>
        /// search on scanboat.com in years 2000 - 2015
        /// </summary>
        public ScbSearch20002015() : base(() =>
            "https://www.scanboat.com/en/boats?page=1&SearchCriteria.BoatTypeID=1&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.PriceFrom=30000.00&SearchCriteria.PriceTo=85000.00&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.LengthFrom=33.00&SearchCriteria.LengthTo=40.00&SearchCriteria.YearIntervalId=117&SearchCriteria.YearFrom=2000.00&SearchCriteria.YearTo=2015.00&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.ExtendedSearch=True&SearchCriteria.Searched=True",
            false
        )
        { }
    }


}
