using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.Scanboat
{
    /// <summary>
    /// The next url from a given search page.
    /// Does not check if there is a next url or not.
    /// </summary>
    public sealed class ScbPageUrl : ScalarEnvelope<Uri>
    {
        /// <summary>
        /// The next url from a given search page.
        /// Does not check if there is a next url or not.
        /// </summary>
        public ScbPageUrl(Uri origin, int pageNumber) : base(() =>
        {
            var url = origin.AbsoluteUri;
            var start = url.IndexOf("page=");
            var end = url.Substring(start).IndexOf("&") + start;

            var urlHead = url.Substring(0, start);
            var urlTail = url.Substring(end, url.Length - end);

            var newUrl = $"{urlHead}page={pageNumber}{urlTail}";

            return new Uri(newUrl);
        })
        { }
    }
}