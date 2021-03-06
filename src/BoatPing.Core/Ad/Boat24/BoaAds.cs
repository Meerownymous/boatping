using System;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.Boat24
{
    /// <summary>
    /// All ads in a boat24.de search.
    /// </summary>
    public sealed class BoaAds : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads in a boat24.de search.
        /// </summary>
        public BoaAds(string url) : this(new TextOf(url))
        { }

        /// <summary>
        /// All ads in a boat24.de search.
        /// </summary>
        public BoaAds(IText search) : base(() =>
            {
                var result = new List<IAd>();
                result.AddRange(
                    new Joined<IAd>(
                        new Mapped<Uri, IEnumerable<IAd>>(
                            searchPage => new BoaPageAds(searchPage),
                            new BoaPages(search)
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
