using System;
using System.Collections.Generic;
using BoatPing.Core.Ad.Scanboat;
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
            using (var page = new ScbPage(startUrl))
            {
                IList<Uri> result = new List<Uri>();

                var pagination = page.FindElement(By.ClassName("pagination"));
                if (
                    new LengthOf(
                        new Filtered<IWebElement>(
                            elem => elem.Text == "Last",
                            pagination.FindElements(By.TagName("a"))
                        )
                    ).Value() > 0
                )
                {
                    var linkToLast =
                        new FirstOf<IWebElement>(
                            new Filtered<IWebElement>(
                                elem => elem.Text == "Last",
                                pagination.FindElements(By.TagName("a"))
                            )
                        ).Value();


                    var lastPageUrl =
                        linkToLast.GetAttribute("href");

                    var pageSegment = lastPageUrl.Substring(lastPageUrl.IndexOf("page=") + "page=".Length);
                    var lastPageNumber = Convert.ToInt32(pageSegment.Substring(0, pageSegment.IndexOf("&")));
                    
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
