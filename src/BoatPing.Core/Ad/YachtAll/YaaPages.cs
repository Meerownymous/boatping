using System;
using System.Collections.Generic;
using BoatPing.Core.Ad.Selenium;
using OpenQA.Selenium;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.YachtAll
{
    /// <summary>
    /// All yachtall pages in a search.
    /// </summary>
    public class YaaPages : ManyEnvelope<Uri>
    {
        /// <summary>
        /// All yachtall pages in a search.
        /// </summary>
        public YaaPages(IText startUrl) : this(() => startUrl.AsString())
        { }

        /// <summary>
        /// All yachtall pages in a search.
        /// </summary>
        public YaaPages(string startUrl) : this(() => startUrl)
        { }

        /// <summary>
        /// All yachtall pages in a search.
        /// </summary>
        public YaaPages(Func<string> startUrl) : base(() =>
        {
            var url = startUrl();
            using (var page = new YaaPage(startUrl, new ChromeHeadless()))
            {
                IList<Uri> result = new List<Uri>();
                var current = new Uri(url);
                result.Add(current);

                var pageNumbers = page.FindElements(By.ClassName("paging-link"));
                if (pageNumbers.Count > 0)
                {
                    var lastPage =
                        new LastOf<int>(
                            new Sorted<int>(
                                new Mapped<IWebElement, int>(
                                    elem =>
                                    {
                                        int result = -1;
                                        int.TryParse(elem.Text, out result);
                                        return result;
                                    },
                                    pageNumbers
                                )
                            )
                        ).Value();

                    for (var pageNumber = 2; pageNumber <= lastPage; pageNumber++)
                    {
                        current = new YaaNextUrl(current).Value();
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
