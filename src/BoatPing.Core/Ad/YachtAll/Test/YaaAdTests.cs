using System;
using BoatPing.Core.Ad.Boot24;
using OpenQA.Selenium;
using Xunit;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;
using Yaapii.Xml;

namespace BoatPing.Core.Ad.YachtAll.Test
{
    public sealed class YaaAdTests
    {
        [Fact]
        public void BuildsID()
        {
            using (var searchPage = new YaaPage("https://www.yachtall.com/de/segelboote/deutschland?lngf=10&lngt=14&ybf=1970&sprcf=15000&sprct=70000"))
            {
                var elem = searchPage.FindElement(By.ClassName("boatlist-subbox"));
                Assert.NotEqual(
                    "error",
                    new YaaAd(elem).ID()
                );
            }
        }

        [Fact]
        public void ExtractsPrice()
        {
            using (var searchPage = new YaaPage("https://www.yachtall.com/de/segelboote/deutschland?lngf=10&lngt=14&ybf=1970&sprcf=15000&sprct=70000"))
            {
                var elem = searchPage.FindElement(By.ClassName("boatlist-subbox"));
                Assert.InRange(
                    new YaaAd(elem).Price(),
                    1, int.MaxValue
                );
            }
        }

        [Fact]
        public void ExtractsCountry()
        {
            using (var searchPage = new YaaPage("https://www.yachtall.com/de/segelboote/deutschland?lngf=10&lngt=14&ybf=1970&sprcf=15000&sprct=70000"))
            {
                var elem = searchPage.FindElement(By.ClassName("boatlist-subbox"));
                Assert.NotEqual(
                    "error",
                    new YaaAd(elem).Content()["country"]
                );
            }
        }

        [Fact]
        public void ExtractsTitle()
        {
            using (var searchPage = new YaaPage("https://www.yachtall.com/de/segelboote/deutschland?lngf=10&lngt=14&ybf=1970&sprcf=15000&sprct=70000"))
            {
                var elem = searchPage.FindElement(By.ClassName("boatlist-subbox"));
                Assert.NotEqual(
                    "error",
                    new YaaAd(elem).Content()["title"]
                );
            }
        }
    }
}