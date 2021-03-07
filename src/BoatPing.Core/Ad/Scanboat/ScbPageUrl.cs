using System;
using System.Web;
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
            var path = new Uri(origin.AbsoluteUri.Substring(0, origin.AbsoluteUri.IndexOf("?")));
            var query = HttpUtility.ParseQueryString(origin.Query);
            query.Set("page", pageNumber.ToString());
            var countryIds = new string[0];
            foreach (var key in query.AllKeys)
            {
                var newValue = query.Get(key);
                query.Remove(key);
                if (key.ToLower() == "SearchCriteria.CountryIds".ToLower())
                {
                    countryIds = newValue.Split(",");
                }
                else
                {
                    query.Set(HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(newValue));
                }
            }

            var uriBuilder = new UriBuilder(path);
            uriBuilder.Query = query.ToString();
            var newUri = uriBuilder.Uri.ToString();

            foreach (var countryId in countryIds)
            {
                newUri += $"&SearchCriteria.CountryIds={countryId}";
            }
            return new Uri(newUri);
        })
        { }
    }
}