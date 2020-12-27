using System;
using System.Collections.Generic;
using System.Globalization;
using BoatPing.Core.Model;
using BriX;
using OpenQA.Selenium;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Boot24.Datum
{
    /// <summary>
    /// A boot24 ad.
    /// </summary>
    public class B24Ad : IAd, IDisposable
    {
        private readonly DateTime found;
        private B24Page page;
        private readonly MapOf attributes;

        /// <summary>
        /// A boot24 ad.
        /// </summary>
        public B24Ad(string url, DateTime found)
        {
            this.page = new B24Page(url);
            this.attributes =
                new MapOf(
                    new KvpOf("title", () =>
                        this.page.FindElement(By.ClassName("detail-img-greybox")).FindElement(By.TagName("h1")).Text
                    ),
                    new KvpOf("price", () =>
                        this.page.FindElement(By.Id("preis")).Text.Replace(".", "")
                    )
                );
            this.found = found;
        }

        public DateTime Found()
        {
            return found;
        }

        public string ID()
        {
            return $"boot24.de:{this.page.FindElement(By.Id("onr")).Text}";
        }

        public string Title()
        {
            return this.attributes["title"];
        }

        public double Price()
        {
            return Convert.ToDouble(this.attributes["price"], CultureInfo.InvariantCulture);
        }

        public IEnumerable<string> Pix()
        {
            throw new NotImplementedException();
        }

        public IBrix Printed()
        {
            throw new NotImplementedException();
        }

        private IDictionary<string, string> Props()
        {
            return this.attributes;
        }

        public void Dispose()
        {
            this.page.Dispose();
        }
    }
}
