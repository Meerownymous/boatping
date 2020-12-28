using System;
using System.Collections;
using System.Collections.Generic;

namespace BoatPing.Core.Model
{
    public interface ILogBook
    {
        bool Contains(IAd ad);
        void Record(IAd ad);
        IList<IAd> RecordsOf(IAd ad);
    }
}
