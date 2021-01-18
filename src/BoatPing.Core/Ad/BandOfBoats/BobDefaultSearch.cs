using System;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.BandOfBoats
{
    /// <summary>
    /// Defsault search for band of boats
    /// </summary>
    public sealed class BobDefaultSearch : TextEnvelope
    {
        /// <summary>
        /// Defsault search for band of boats
        /// </summary>s
        public BobDefaultSearch() : base(() =>
            "https://www.bandofboats.com/de/boot-kaufen?ref_nature%5B0%5D=sailing_boat&ref_category%5B0%5D=9&page=1&price_min=20000&price_max=60000&year_min=1970&year_max=&loa_min=10&loa_max=&beam_min=&beam_max=&horse_power_min=&horse_power_max=",
            false
        )
        { }
    }
}
