using System;
using System.Collections;
using System.Collections.Generic;

namespace BoatPing.Core
{
    /// <summary>
    /// Logbook where all ads and new versions of them are recorded.
    /// </summary>
    public interface ILogBook
    {
        /// <summary>
        /// Does the book contain any versions of the ad?
        /// </summary>
        bool Contains(IAd ad);

        /// <summary>
        /// Does the book contain this exact version of the ad?
        /// </summary>
        bool ContainsVersion(IAd ad);

        /// <summary>
        /// Record this version. Will not make duplicate entries.
        /// </summary>
        void Record(IAd ad);

        /// <summary>
        /// All versions for the ad.
        /// </summary>
        IList<IAd> RecordsOf(IAd ad);
    }
}
