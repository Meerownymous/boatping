﻿using System;
using System.Collections.Generic;
using BoatPing.Core.Ad.Selenium;
using BoatPing.Core.Boot24;
using OpenQA.Selenium;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core.Ad.Boat24
{
    /// <summary>
    /// All boat24 pages in a search.
    /// </summary>
    public class BoaPages : ManyEnvelope<Uri>
    {
        /// <summary>
        /// All boat24 pages in a search.
        /// </summary>
        public BoaPages(IText startUrl) : this(() => startUrl.AsString())
        { }

        /// <summary>
        /// All boat24 pages in a search.
        /// </summary>
        public BoaPages(string startUrl) : this(() => startUrl)
        { }

        /// <summary>
        /// All boat24 pages in a search.
        /// </summary>
        public BoaPages(Func<string> startUrl) : base(() =>
        {
            var url = startUrl();
            using (var page = new BoaPage(startUrl, new ChromeHeadless()))
            {
                IList<Uri> result = new List<Uri>();
                var current = new Uri(url);
                result.Add(current);

                var pageNumbers = page.FindElements(By.ClassName("pagination__page"));
                if (pageNumbers.Count > 0)
                {
                    var lastPage =
                        new LastOf<int>(
                            new Sorted<int>(
                                new Mapped<IWebElement, int>(
                                    elem =>
                                    {
                                        var result = -1;
                                        int.TryParse(elem.Text, out result);
                                        return result;
                                    },
                                    pageNumbers
                                )
                            )
                        ).Value();

                    
                    for (var pageNumber = 2; pageNumber <= lastPage; pageNumber++)
                    {
                        current = new BoaNextUrl(current).Value();
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
