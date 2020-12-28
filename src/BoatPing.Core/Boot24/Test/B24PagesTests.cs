using System;
using OpenQA.Selenium.Chrome;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Boot24
{
    public sealed class B24PagesTests
    {
        [Fact]
        public void EnumeratesPages()
        {
            Assert.InRange(
                new LengthOf(
                    new B24Pages(new B24DefaultSearch())
                ).Value(),
                2,
                int.MaxValue
            );
        }
    }
}
