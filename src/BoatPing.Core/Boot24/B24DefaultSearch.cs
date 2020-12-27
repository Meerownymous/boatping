using System;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// Default search query for Boot24.de
    /// </summary>
    public sealed class B24DefaultSearch : TextEnvelope
    {
        /// <summary>
        /// Default search query for Boot24.de
        /// </summary>
        public B24DefaultSearch() : base(() =>
            "https://www.boot24.com/segelboot/#pab=35000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c0",
            false
        )
        { }


        public override string ToString()
        {
            return this.AsString();
        }
    }
}
