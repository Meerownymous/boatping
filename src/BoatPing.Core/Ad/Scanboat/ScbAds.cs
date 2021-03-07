using System;
using System.Collections.Generic;
using BoatPing.Core.Ad.Boat24;
using BoatPing.Core.Ad.Scanboat;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.Scanboat
{
    /// <summary>
    /// All ads in a scanboat.com search.
    /// </summary>
    public sealed class ScbAds : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads in a scanboat.com search.
        /// </summary>
        public ScbAds(string url, int minPrice, int maxPrice) : this(new TextOf(url), minPrice, maxPrice)
        { }

        /// <summary>
        /// All ads in a scanboat.com search.
        /// </summary>
        public ScbAds(IText search, int minPrice, int maxPrice) : base(() =>
            {
                var result = new List<IAd>();
                result.AddRange(
                    new PriceFiltered(minPrice, maxPrice,
                        new Joined<IAd>(
                            new Mapped<Uri, IEnumerable<IAd>>(
                                searchPage => new ScbPageAds(searchPage),
                                new ScbPages(search)
                            )
                        )
                    )
                );
                return result;
            },
            false
        )
        { }

        public override string ToString()
        {
            return "www.scanboat.com";
        }
    }
}
