using System;
using Xunit;
using Yaapii.Atoms.Scalar;
using Yaapii.Xml;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// Search result from a Boot24.de search.
    /// </summary>
    public class B24SearchResult : XMLEnvelope
    {
        public B24SearchResult() : base(new ScalarOf<IXML>(() => new XMLCursor("<boats><boat>bob</boat></boats>")))
        {
        }
    }

    public sealed class B24SearchResultTests
    {
        [Fact]
        public void Works()
        {
            throw new Exception("Doesnt");
        }
    }
}
