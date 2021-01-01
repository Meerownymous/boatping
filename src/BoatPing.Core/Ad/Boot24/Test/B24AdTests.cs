using System;
using BoatPing.Core.Ad.Boot24;
using OpenQA.Selenium;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core.Boot24
{
    public sealed class B24AdTests
    {
        [Fact]
        public void BuildsID()
        {
            using (var searchPage = new B24Page(new B24DefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("sr-objektbox-in"));
                Assert.NotEmpty(new B24Ad(elem).ID());
            }
        }

        [Fact]
        public void ExtractsPrice()
        {
            using (var searchPage = new B24Page(new B24DefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("sr-objektbox-in"));
                Assert.InRange(
                    new B24Ad(elem).Price(),
                    1, int.MaxValue
                );
            }
        }

        [Fact]
        public void ExtractsCountry()
        {
            using (var searchPage = new B24Page(new B24DefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("sr-objektbox-in"));
                Assert.NotEqual(
                    "error",
                    new B24Ad(elem).Content()["country"]
                );
            }
        }
    }
}