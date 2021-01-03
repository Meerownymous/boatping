using System;
using System.Collections.Generic;
using BoatPing.Core.Model;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core.LogBook
{
    /// <summary>
    /// Envelope for logbooks.
    /// </summary>
    public class LogBookEnvelope : ILogBook
    {
        private ScalarOf<ILogBook> book;

        /// <summary>
        /// Envelope for logbooks.
        /// </summary>
        public LogBookEnvelope(Func<ILogBook> book)
        {
            this.book = new ScalarOf<ILogBook>(book);
        }

        public bool Contains(IAd ad)
        {
            return this.book.Value().Contains(ad);
        }

        public bool ContainsVersion(IAd ad)
        {
            return this.book.Value().ContainsVersion(ad);
        }

        public void Record(IAd ad)
        {
            this.book.Value().Record(ad);
        }

        public IList<IAd> RecordsOf(IAd ad)
        {
            return this.book.Value().RecordsOf(ad);
        }
    }
}
