using System;
using System.Linq;
using System.Xml.Linq;
using OpenQA.Selenium.Chrome;
using Xunit;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;
using Yaapii.Http;
using Yaapii.Http.Fake;
using Yaapii.Http.Parts.Bodies;
using Yaapii.Http.Parts.Uri;
using Yaapii.Http.Requests;
using Yaapii.Http.Responses;
using Yaapii.Http.Verifications;
using Yaapii.Http.Wires;
using Yaapii.JSON;
using Yaapii.Xml;

namespace BoatPing.Core.Boot24
{
    public sealed class B24SearchResultTests
    {
        [Fact]
        public void ExtractsContent()
        {
            Assert.Contains(
                "<div class=\"main-content-norm\">",
                new B24SearchResult(
                    "default-de",
                    1,
                    new FkWire(
                        200,
                        "",
                        new Body(new ResourceOf("Boot24/Datum/search.example-page-1.xml", typeof(B24SearchResultTests)))
                    )
                )
                .Value()
                .Text
                .ToString()
            );
        }

        [Fact]
        public async void KnowsItHasMorePages()
        {
            var html = new TextOf(new ResourceOf("Boot24/Datum/javascript.example.html", typeof(B24SearchResult))).AsString();
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://www.boot24.de");
            }
                

            //var text = driver.FindElementById("demo").Text;

            //Assert.Equal("JS Executed", text);
            //driver.Close();

            //var web = new Microsoft.Web.WebView2.WinForms.WebView2();
            //web.Source = new Uri("http://www.boot24.de");

            //Assert.True(
            //    new B24SearchResult.HasMorePages(
            //        new FkSearchResult("search.example-page-1.xml")
            //    )
            //);
        }
    }
}
