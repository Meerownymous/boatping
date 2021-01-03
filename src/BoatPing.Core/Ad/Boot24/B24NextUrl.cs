using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// The next url from a given search page.
    /// Does not check if there is a next url or not.
    /// </summary>
    public sealed class B24NextUrl : ScalarEnvelope<Uri>
    {
        /// <summary>
        /// The next url from a given search page.
        /// Does not check if there is a next url or not.
        /// </summary>
        public B24NextUrl(Uri origin) : base(() =>
        {
            var url = origin.AbsoluteUri;
            var start = url.IndexOf("segelboot/") + "segelboot/".Length;
            var end = url.IndexOf("#");

            var urlHead = url.Substring(0, start);
            var urlTail = url.Substring(end, url.Length - end);

            var pageSegment =
                new Replaced(
                    new Replaced(
                        new Replaced(
                            new TextOf(url),
                            urlHead, ""
                        ), urlTail, ""
                    ), "?page=", ""
                ).AsString();

            if (String.IsNullOrEmpty(pageSegment))
            {
                pageSegment = "1";
            }
            var number = Convert.ToInt32(pageSegment);
            var newUrl = $"{urlHead}?page={number + 1}{urlTail}";

            return new Uri(newUrl);
        })
        { }
    }
}