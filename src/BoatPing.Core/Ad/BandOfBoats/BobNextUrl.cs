﻿using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.BandOfBoats
{
    /// <summary>
    /// The next url from a given search page.
    /// Does not check if there is a next url or not.
    /// </summary>
    public sealed class BobNextUrl : ScalarEnvelope<Uri>
    {
        /// <summary>
        /// The next url from a given search page.
        /// Does not check if there is a next url or not.
        /// </summary>
        public BobNextUrl(Uri origin) : base(() =>
        {
            var url = origin.AbsoluteUri;
            var start = url.IndexOf("page=");
            var end = url.Substring(start).IndexOf("&") + start;

            var urlHead = url.Substring(0, start);
            var urlTail = url.Substring(end, url.Length - end);

            var pageSegment =
                new Replaced(
                    new Replaced(
                        new Replaced(
                            new TextOf(url),
                            urlHead, ""
                        ), urlTail, ""
                    ), "page=", ""
                ).AsString();

            var number = Convert.ToInt32(pageSegment);
            var newUrl = $"{urlHead}page={number+1}{urlTail}";

            return new Uri(newUrl);
        })
        { }
    }
}