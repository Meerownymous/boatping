    using System;
    using System.Collections.Generic;
    using BoatPing.Core.Ad.Scanboat;
    using BoatPing.Core.Ad.Selenium;
    using BoatPing.Core.Boot24;
    using OpenQA.Selenium;
    using Yaapii.Atoms;
    using Yaapii.Atoms.Enumerable;
    using Yaapii.Atoms.Scalar;

    namespace BoatPing.Core.Ad.Scanboat
    {
        /// <summary>
        /// All scanboat pages in a search.
        /// </summary>
        public class ScbPages : ManyEnvelope<Uri>
        {
            /// <summary>
            /// All scanboat pages in a search.
            /// </summary>
            public ScbPages(IText startUrl) : this(() => startUrl.AsString())
            { }

            /// <summary>
            /// All scanboat pages in a search.
            /// </summary>
            public ScbPages(string startUrl) : this(() => startUrl)
            { }

            /// <summary>
            /// All scanboat pages in a search.
            /// </summary>
            public ScbPages(Func<string> startUrl) : base(() =>
            {
                var url = startUrl();
                using (var page = new ScbPage(startUrl, new ChromeHeadless()))
                {
                    IList<Uri> result = new List<Uri>();

                    var pagination = page.FindElement(By.ClassName("pagination"));
                    var lastPageNumber = -1;

                    foreach (var pageLink in pagination.FindElements(By.TagName("a")))
                    {
                        var link = pageLink.GetAttribute("href");
                        if (link.Contains("?page="))
                        {
                            link = link.Substring(link.IndexOf("?page=") + "?page=".Length);
                            var linkPage = 0;
                            var numberString = link.Substring(0, link.IndexOf("&"));
                            if(int.TryParse(numberString, out linkPage))
                            {
                                if(linkPage > lastPageNumber)
                                {
                                    lastPageNumber = linkPage;
                                }
                            }
                        }
                    }

                    if (lastPageNumber > -1)
                    {
                        for (var pageNumber = 1; pageNumber <= lastPageNumber; pageNumber++)
                        {
                            result.Add(new ScbPageUrl(new Uri(url), pageNumber).Value());
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
