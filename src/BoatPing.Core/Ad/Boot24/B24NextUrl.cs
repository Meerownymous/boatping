using System;
using System.Web;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;
using System.Linq;

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
            var path = origin;
            if (origin.AbsoluteUri.IndexOf("?") > 0)
            {
                path = new Uri(origin.AbsoluteUri.Substring(0, origin.AbsoluteUri.IndexOf("?")));
            }
            var query = HttpUtility.ParseQueryString(origin.Query);

            var page = 1;
            if (query.AllKeys.Contains("page"))
            {
                page = Convert.ToInt32(query.Get("page"));
            }
            page++;
            query.Set("page", page.ToString());

            foreach (var key in query.AllKeys)
            {
                var newValue = query.Get(key);
                query.Remove(key);
                query.Set(HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(newValue));
            }

            var uriBuilder = new UriBuilder(path);
            uriBuilder.Query = query.ToString();
            uriBuilder.Fragment = origin.Fragment;

            var newUri = uriBuilder.Uri;

            return newUri;
        })
        { }
    }
}