using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad
{
    /// <summary>
    /// Given ads filtered by price.
    /// </summary>
    public class PriceFiltered : ManyEnvelope<IAd>
    {
        /// <summary>
        /// Given ads filtered by price.
        /// </summary>
        public PriceFiltered(int priceMin, int priceMax, IEnumerable<IAd> origin) : base(() =>
            new Filtered<IAd>(
                ad => ad.Price() >= priceMin && ad.Price() <= priceMax,
                origin
            ),
            false
        )
        { }
    }
}
