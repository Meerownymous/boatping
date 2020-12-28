using System;
using Xunit;

namespace BoatPing.Core.Boot24.Test
{
    public sealed class B24NextUrlTests
    {
        [Fact]
        public void BuildsFromPageSegment()
        {
            Assert.Equal(
                "https://www.boot24.com/segelboot/?page=2#pab=35000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c0",
                new B24NextUrl(new Uri("https://www.boot24.com/segelboot/?page=1#pab=35000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c0"))
                    .Value()
                    .AbsoluteUri
            );
        }

        [Fact]
        public void BuildsWithoutPageSegment()
        {
            Assert.Equal(
                "https://www.boot24.com/segelboot/?page=2#pab=35000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c0",
                new B24NextUrl(new Uri("https://www.boot24.com/segelboot/#pab=35000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c0"))
                    .Value()
                    .AbsoluteUri
            );
        }
    }
}
