using System;
using OpenQA.Selenium;
using Xunit;

namespace BoatPing.Core.Boot24
{
    public sealed class B24NextPageTests
    {
        [Fact]
        public void NavigatesToNext()
        {
            using (var page = new B24Page(new B24DefaultSearch()))
            {
                var before = Convert.ToInt32(page.FindElement(By.CssSelector(".seite.selected")).Text);

                Assert.Equal(
                    before + 1,
                    Convert.ToInt32(
                        new B24NextPage(page)
                            .FindElement(By.CssSelector(".seite.selected")).Text
                        )
                );
            }
        }
    }
}
