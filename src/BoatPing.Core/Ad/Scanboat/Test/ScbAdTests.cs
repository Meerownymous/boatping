using System;
using BoatPing.Core.Ad.Boot24;
using OpenQA.Selenium;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core.Ad.Scanboat.Test
{
    public sealed class ScbAdTests
    {
        [Fact]
        public void BuildsID()
        {
            using (var searchPage = new ScbPage(new ScbSearch19701985()))
            {
                var elem =
                    searchPage
                        .FindElement(By.ClassName("boat-list__body"))
                        .FindElement(By.ClassName("item"));
                Assert.NotEqual(
                    "error",
                    new ScbAd(elem).ID()
                );
            }
        }

        [Fact]
        public void ExtractsPrice()
        {
            using (var searchPage = new ScbPage(new ScbSearch19701985()))
            {
                var elem =
                    searchPage
                        .FindElement(By.ClassName("boat-list__body"))
                        .FindElement(By.ClassName("item"));
                Assert.InRange(
                    new ScbAd(elem).Price(),
                    1, int.MaxValue
                );
            }
        }

        [Fact]
        public void ExtractsCountry()
        {
            using (var searchPage = new ScbPage(new ScbSearch19701985()))
            {
                var elem =
                    searchPage
                        .FindElement(By.ClassName("boat-list__body"))
                        .FindElement(By.ClassName("item"));

                Assert.NotEqual(
                    "error",
                    new ScbAd(elem).Content()["country"]
                );
            }
        }

        [Fact]
        public void ExtractsTitle()
        {
            using (var searchPage = new ScbPage(new ScbSearch19701985()))
            {
                var elem =
                    searchPage
                        .FindElement(By.ClassName("boat-list__body"))
                        .FindElement(By.ClassName("item"));

                Assert.NotEqual(
                    "error",
                    new ScbAd(elem).Content()["title"]
                );
            }
        }
    }
}