﻿using System;
using System.Web;
using System.Linq;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.Boat24
{
    /// <summary>
    /// The next url from a given search page.
    /// Does not check if there is a next url or not.
    /// </summary>
    public sealed class BoaNextUrl : ScalarEnvelope<Uri>
    {
        private const int ADS_ON_PAGE = 20;

        /// <summary>
        /// The next url from a given search page.
        /// Does not check if there is a next url or not.
        /// </summary>
        public BoaNextUrl(Uri origin) : base(() =>
        {
            var path = new Uri(origin.AbsoluteUri.Substring(0, origin.AbsoluteUri.IndexOf("?")));
            var query = HttpUtility.ParseQueryString(origin.Query);

            var page = 0;
            if (query.AllKeys.Contains("page"))
            {
                page = Convert.ToInt32(query.Get("page"));
            }
            page+=ADS_ON_PAGE;
            query.Set("page", page.ToString());

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