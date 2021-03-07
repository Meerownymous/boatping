using System;
using OpenQA.Selenium.Chrome;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Boot24.Test
{
    public sealed class B24PagesTests
    {
        [Fact]
        public void EnumeratesPages()
        {
            Assert.InRange(
                new LengthOf(
                    new B24Pages(
                        "https://www.boot24.com/segelboot/#pab=20000&pbis=58000&jahrvon=1970&lmin=10&lmax=13&land=c0"
                    )
                ).Value(),
                2,
                int.MaxValue
            );
        }

        [Fact]
        public void WorksWithEmptySearch()
        {
            Assert.Equal(
                0,
                new LengthOf(
                    new B24Pages("https://www.boot24.com/segelboot/#pab=10&lmin=10&lmax=13&land=c0&pbis=11")
                ).Value()
            );
        
        }
    }
}
