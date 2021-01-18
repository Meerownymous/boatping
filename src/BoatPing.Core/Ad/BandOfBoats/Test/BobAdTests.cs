using System;
using BoatPing.Core.Ad.Boot24;
using OpenQA.Selenium;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core.Ad.BandOfBoats.Test
{
    public sealed class BoaAdTests
    {
        [Fact]
        public void BuildsID()
        {
            using (var searchPage = new BobPage(new BobDefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("bs-card"));
                Assert.NotEqual(
                    "error",
                    new BobAd(elem).ID()
                );
            }
        }

        [Fact]
        public void ExtractsPrice()
        {
            using (var searchPage = new BobPage(new BobDefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("bs-card"));
                Assert.InRange(
                    new BobAd(elem).Price(),
                    1, int.MaxValue
                );
            }
        }

        [Fact]
        public void ExtractsCountry()
        {
            using (var searchPage = new BobPage(new BobDefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("bs-card"));
                Assert.NotEqual(
                    "error",
                    new BobAd(elem).Content()["country"]
                );
            }
        }

        [Fact]
        public void ExtractsTitle()
        {
            using (var searchPage = new BobPage(new BobDefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("bs-card"));
                Assert.NotEqual(
                    "error",
                    new BobAd(elem).Content()["title"]
                );
            }
        }
    }
}