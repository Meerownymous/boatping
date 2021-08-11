using System;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.YachtAll
{
    /// <summary>
    /// All ads in a YachtAll.com search.
    /// </summary>
    public sealed class YaaAds : ManyEnvelope<IAd>
    {
        /// <summary>
        /// All ads in a YachtAll.com search.
        /// </summary>
        public YaaAds(string url) : this(new TextOf(url))
        { }

        /// <summary>
        /// All ads in a YachtAll.com search.
        /// </summary>
        public YaaAds(IText search) : base(() =>
            {
                var result = new List<IAd>();
                result.AddRange(
                    new Joined<IAd>(
                        new Mapped<Uri, IEnumerable<IAd>>(
                            searchPage => new YaaPageAds(searchPage),
                            new YaaPages(search)
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
            return "www.yachtall.com";
        }
    }
}
