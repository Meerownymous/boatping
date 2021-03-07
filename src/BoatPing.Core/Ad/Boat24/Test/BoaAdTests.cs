using System;
using BoatPing.Core.Ad.Boot24;
using OpenQA.Selenium;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core.Ad.Boat24.Test
{
    public sealed class BoaAdTests
    {
        [Fact]
        public void BuildsID()
        {
            using (var searchPage = new BoaPage(new BoaDefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("blurb--strip"));
                Assert.NotEqual(
                    "error",
                    new BoaAd(elem).ID()
                );
            }
        }

        [Fact]
        public void ExtractsPrice()
        {
            using (var searchPage = new BoaPage(new BoaDefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("blurb--strip"));
                Assert.InRange(
                    new BoaAd(elem).Price(),
                    1, int.MaxValue
                );
            }
        }

        [Fact]
        public void ExtractsCountry()
        {
            using (var searchPage = new BoaPage(new BoaDefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("blurb--strip"));
                Assert.NotEqual(
                    "error",
                    new BoaAd(elem).Content()["country"]
                );
            }
        }

        [Fact]
        public void ExtractsTitle()
        {
            using (var searchPage = new BoaPage(new BoaDefaultSearch()))
            {
                var elem = searchPage.FindElement(By.ClassName("blurb--strip"));
                Assert.NotEqual(
                    "error",
                    new BoaAd(elem).Content()["title"]
                );
            }
        }
    }
}