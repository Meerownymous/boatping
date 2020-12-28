using System;
using System.Collections.Generic;
using BoatPing.Core.Model;

namespace BoatPing.Core.LogBook
{
    /// <summary>
    /// A logbook that exists in ram.
    /// </summary>
    public sealed class RamLogBook : LogBookEnvelope
    {
        /// <summary>
        /// A logbook that exists in ram.
        /// </summary>
        public RamLogBook() : base(() => new XiveLogBook(new RamHive("root")))
        { }
    }
}
