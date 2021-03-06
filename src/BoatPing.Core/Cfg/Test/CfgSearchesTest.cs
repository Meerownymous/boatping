﻿using System;
using System.IO;
using BoatPing.Core.Ad.Boat24;
using BoatPing.Core.Ad.Scanboat;
using BoatPing.Core.Boot24;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;

namespace BoatPing.Core.Cfg.Test
{
    public sealed class CfgSearchesTest
    {
        [Fact]
        public void ParsesSearches()
        {
            using(var cfg = new TempDirectory())
            {
                var path = Path.Combine(cfg.Value().FullName, "searches.cfg");
                File.WriteAllLines(
                    path,
                    new string[]
                    {
                        "price-min 0",
                        "price-max 59000",
                        //band of boats DE
                        "https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=DE&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=",
                        //band of boats NL
                        "https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=NL&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14beam_min=&beam_max=&horse_power_min=&horse_power_max=",
                        //band of boats BE
                        "https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=BE&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=",
                        //"band of boats FR"
                        "https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=FR&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=",
                        //"band of boats DK"
                        "https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=DK&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=",
                        //band of boats UK
                        "https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=GB&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=",
                        //band of boats NW
                        "https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=NO&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=",
                        //band of boats SE
                        "https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B%5D=sailing_boat&page=1&ref_category%5B%5D=9&country%5B%5D=SE&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=14&beam_min=&beam_max=&horse_power_min=&horse_power_max=",
                        //scanboat 1970-1985
                        "https://www.scanboat.com/en/boats?page=1&SearchCriteria.BoatTypeID=1&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.PriceFrom=30000.00&SearchCriteria.PriceTo=85000.00&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.LengthFrom=33.00&SearchCriteria.LengthTo=40.00&SearchCriteria.YearIntervalId=110&SearchCriteria.YearFrom=1970.00&SearchCriteria.YearTo=1985.00&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.ExtendedSearch=True&SearchCriteria.Searched=True",
                        //scanboat 1985-1995
                        "https://www.scanboat.com/en/boats?page=1&SearchCriteria.BoatTypeID=1&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.PriceFrom=30000.00&SearchCriteria.PriceTo=85000.00&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.LengthFrom=33.00&SearchCriteria.LengthTo=40.00&SearchCriteria.YearIntervalId=111&SearchCriteria.YearFrom=1985.00&SearchCriteria.YearTo=1995.00&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.ExtendedSearch=True&SearchCriteria.Searched=True",
                        //scanboat 1995-2000
                        "https://www.scanboat.com/en/boats?page=1&SearchCriteria.BoatTypeID=1&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.PriceFrom=30000.00&SearchCriteria.PriceTo=85000.00&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.LengthFrom=33.00&SearchCriteria.LengthTo=40.00&SearchCriteria.YearIntervalId=112&SearchCriteria.YearFrom=1995.00&SearchCriteria.YearTo=2000.00&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.ExtendedSearch=True&SearchCriteria.Searched=True",
                        //scanboat 2000-2015
                        "https://www.scanboat.com/en/boats?page=1&SearchCriteria.BoatTypeID=1&SearchCriteria.CurrencyID=2&SearchCriteria.PriceIntervalId=389&SearchCriteria.PriceFrom=30000.00&SearchCriteria.PriceTo=85000.00&SearchCriteria.LengthWidthUnitID=2&SearchCriteria.LengthIntervalId=87&SearchCriteria.LengthFrom=33.00&SearchCriteria.LengthTo=40.00&SearchCriteria.YearIntervalId=117&SearchCriteria.YearFrom=2000.00&SearchCriteria.YearTo=2015.00&SearchCriteria.CountryIds=12&SearchCriteria.CountryIds=1&SearchCriteria.CountryIds=2&SearchCriteria.CountryIds=10&SearchCriteria.CountryIds=3&SearchCriteria.CountryIds=6&SearchCriteria.CountryIds=8&SearchCriteria.ExtendedSearch=True&SearchCriteria.Searched=True",
                        //boat24 DE FR NL DK BE
                        "https://www.boat24.com/en/sailboats/?jhr_min=1970&lge_max=14&lge_min=10&page=0&prs_max=59000&prs_min=20000&rgo%5B0%5D=2&rgo%5B1%5D=41&rgo%5B2%5D=11&rgo%5B3%5D=15&rgo%5B4%5D=4&typ%5B0%5D=230&whr=EUR",
                        //boat24 BE SW NW UK
                        "https://www.boat24.com/en/sailboats/?src=&typ%5B%5D=230&page=0&cem=&whr=EUR&prs_min=20000&prs_max=59000&lge_min=10&lge_max=14&bre_min=&bre_max=&tie_min=&tie_max=&gew_min=&gew_max=&jhr_min=1970&jhr_max=&lei_min=&lei_max=&ant=&rgo%5B%5D=11&rgo%5B%5D=49&rgo%5B%5D=43&rgo%5B%5D=24&kie=&mob=&per_min=&per_max=&cab_min=&cab_max=&ber_min=&ber_max=&hdr_min=&hdr_max=&sort=rand",
                        //boot24 DE
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c0",
                        //boot24 NL
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c25",
                        //"boot24 FR
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c10",
                        //boot24 GB
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c39",
                        //boot24 PL
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c27",
                        //boot24 BE
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c5",
                        //boot24 DK
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c8",
                        //boot24 IR
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c13",
                        //boot24 NW
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c6",
                        //boot24 SW
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c31",
                        //unsupported
                        "www.google.de",
                        "#comment"

                    }
                );

                Assert.Equal(
                    24,
                    new Yaapii.Atoms.Enumerable.LengthOf(
                        new CfgSearches(
                            new Uri(path),
                            new ListOf<string>(
                                "bandofboats.com",
                                "boot24.com",
                                "boat24.com",
                                "scanboat.com"
                            ),
                            error => { }
                        )
                    ).Value()
                );
            }
        }
    }
}
