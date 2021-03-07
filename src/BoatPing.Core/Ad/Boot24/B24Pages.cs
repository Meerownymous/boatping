using System;
using System.Collections.Generic;
using BoatPing.Core.Ad.Selenium;
using OpenQA.Selenium;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Http.Responses;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// All boot24 pages in a search.
    /// </summary>
    public class B24Pages : ManyEnvelope<Uri>
    {
        /// <summary>
        /// All boot24 pages in a search.
        /// </summary>
        public B24Pages(IText startUrl) : this(() => startUrl.AsString())
        { }

        /// <summary>
        /// All boot24 pages in a search.
        /// </summary>
        public B24Pages(string startUrl) : this(() => startUrl)
        { }

        /// <summary>
        /// All boot24 pages in a search.
        /// </summary>
        public B24Pages(Func<string> startUrl) : base(() =>
            {
                var url = startUrl();
                using (var page = new B24Page(startUrl, new ChromeHeadless()))
                {
                    IList<Uri> result = new List<Uri>();

                    var pageNumbers = page.FindElements(By.ClassName("seite"));
                    if (pageNumbers.Count > 0)
                    {

                        var lastPage =
                            new LastOf<int>(
                                new Sorted<int>(
                                    new Mapped<IWebElement, int>(
                                        elem => Convert.ToInt32(elem.Text),
                                        pageNumbers
                                    )
                                )
                            ).Value();

                        var current = new Uri(url);
                        result.Add(current);
                        for (var pageNumber = 2; pageNumber <= lastPage; pageNumber++)
                        {
                            current = new B24NextUrl(current).Value();
                            result.Add(current);
                        }
                    }
                    return result;
                }
            },
            false
        )
        { }
    }
}
