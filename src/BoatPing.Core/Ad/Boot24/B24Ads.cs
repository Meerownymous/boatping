using System;
using System.Collections.Generic;
using BoatPing.Core.Ad;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// All ads in a boot24.de search.
    /// </summary>
    public sealed class B24Ads : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads in a boot24.de search.
        /// </summary>
        public B24Ads(string url, int minPrice, int maxPrice) : this(new TextOf(url), minPrice, maxPrice)
        { }

        /// <summary>
        /// All ads in a boot24.de search.
        /// </summary>
        public B24Ads(IText search, int minPrice, int maxPrice) : base(() =>
            {
                var result = new List<IAd>();
                result.AddRange(
                    new PriceFiltered(15000, 60000,
                        new Joined<IAd>(
                            new Mapped<Uri, IEnumerable<IAd>>(
                                searchPage => new B24PageAds(searchPage),
                                new B24Pages(search)
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
            return "www.boot24.de";
        }
    }
}
