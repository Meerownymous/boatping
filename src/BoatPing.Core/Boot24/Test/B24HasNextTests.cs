using System;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace BoatPing.Core.Boot24
{
    public sealed class B24HasNextTests
    {
        [Fact]
        public void KnowsItHasNext()
        {
            using (var page = new ChromeDriver())
            {
                Assert.True(new B24HasNext(new B24Page(new B24DefaultSearch(), page)));
            }
        }

        [Fact]
        public void KnowsItDoesNotHaveMorePages()
        {
            using (var page = new ChromeDriver())
            {
                page.Navigate().GoToUrl("https://www.boot24.com/segelboot/#pab=10&pbis=11&jahrvon=1970&lmin=10&lmax=13&land=c0");

                new B24WaitForStable(page, new TimeSpan(0, 0, 30)).Invoke();
                Assert.False(new B24HasNext(page));
            }
        }
    }
}
