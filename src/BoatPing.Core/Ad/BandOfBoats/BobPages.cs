using System;
using System.Collections.Generic;
using BoatPing.Core.Ad.Selenium;
using OpenQA.Selenium;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.BandOfBoats
{
    /// <summary>
    /// All boat24 pages in a search.
    /// </summary>
    public class BobPages : ManyEnvelope<Uri>
    {
        /// <summary>
        /// All boat24 pages in a search.
        /// </summary>
        public BobPages(IText startUrl) : this(() => startUrl.AsString())
        { }

        /// <summary>
        /// All boat24 pages in a search.
        /// </summary>
        public BobPages(string startUrl) : this(() => startUrl)
        { }

        /// <summary>
        /// All boat24 pages in a search.
        /// </summary>
        public BobPages(Func<string> startUrl) : base(() =>
        {
            var url = startUrl();
            using (var page = new BobPage(startUrl, new ChromeHeadless()))
            {
                IList<Uri> result = new List<Uri>();

                var pageNumbers = page.FindElements(By.ClassName("page-link"));
                if (pageNumbers.Count > 0)
                {
                    var lastPage =
                        new LastOf<int>(
                            new Sorted<int>(
                                new Filtered<int>(
                                    pageNumber => pageNumber > -1,
                                    new Mapped<IWebElement, int>(
                                        elem =>
                                        {
                                            int result = -1;
                                            try
                                            {
                                                result = Convert.ToInt32(elem.Text);
                                            }
                                            catch (Exception)
                                            {

                                            }
                                            return result;
                                        },
                                        pageNumbers
                                    )
                                )
                            )
                        ).Value();

                    var current = new Uri(url);
                    result.Add(current);
                    for (var pageNumber = 2; pageNumber <= lastPage; pageNumber++)
                    {
                        current = new BobNextUrl(current).Value();
                        result.Add(current);
                    }
                }
                else
                {
                    result.Add(new Uri(url));
                }
                return result;
            }
        },
            false
        )
        { }
    }
}
