using System;
using System.Web;
using System.Linq;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.YachtAll
{
    /// <summary>
    /// The next url from a given search page.
    /// Does not check if there is a next url or not.
    /// </summary>
    public sealed class YaaNextUrl : ScalarEnvelope<Uri>
    {
        /// <summary>
        /// The next url from a given search page.
        /// Does not check if there is a next url or not.
        /// </summary>
        public YaaNextUrl(Uri origin) : base(() =>
        {
            var path = new Uri(origin.AbsoluteUri.Substring(0, origin.AbsoluteUri.IndexOf("?")));
            var query = HttpUtility.ParseQueryString(origin.Query);

            var page = 1;
            if (query.AllKeys.Contains("pg"))
            {
                page = Convert.ToInt32(query.Get("pg"));
            }

            page++;
            query.Set("pg", page.ToString());

            foreach (var key in query.AllKeys)
            {
                var newValue = query.Get(key);
                query.Remove(key);
                query.Set(HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(newValue));
            }

            var uriBuilder = new UriBuilder(path);
            uriBuilder.Query = query.ToString();
            var newUri = uriBuilder.Uri;

            return newUri;
        })
        { }
    }
}