using System;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.BandOfBoats
{
    /// <summary>
    /// All ads in a bandofboats.com search.
    /// </summary>
    public sealed class BobAds : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads in a bandofboats.com search.
        /// </summary>
        public BobAds(string url) : this(new TextOf(url))
        { }

        /// <summary>
        /// All ads in a bandofboats.com search.
        /// </summary>
        public BobAds(IText search) : base(() =>
            {
                var result = new List<IAd>();
                result.AddRange(
                    new Joined<IAd>(
                        new Mapped<Uri, IEnumerable<IAd>>(
                            searchPage => new BobPageAds(searchPage),
                            new BobPages(search)
                        )
                    )
                );
                return result;

            },
            false
        )
        { }
    }
}
