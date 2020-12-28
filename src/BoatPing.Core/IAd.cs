using System;
using System.Collections.Generic;
using BriX;

namespace BoatPing.Core
{
    /// <summary>
    /// A boat ad.
    /// </summary>
    public interface IAd
    {
        /// <summary>
        /// Deeplink to the ad.
        /// </summary>
        /// <returns></returns>
        string Url();

        /// <summary>
        /// Global unique ID for this ad.
        /// </summary>
        string ID();

        /// <summary>
        /// Source platform identifier of this ad.
        /// </summary>
        string Source();

        /// <summary>
        /// Price for the boat.
        /// </summary>
        /// <returns></returns>
        double Price();

        /// <summary>
        /// Additional content of the ad.
        /// </summary>
        IDictionary<string, string> Content();
    }
}
