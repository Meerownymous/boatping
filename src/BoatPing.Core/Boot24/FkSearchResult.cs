using System;
using System.Xml.Linq;
using HtmlAgilityPack;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;
using Yaapii.Xml;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// Fake seachresult from boot24 datum.
    /// </summary>
    public class FkSearchResult : ScalarEnvelope<HtmlDocument>
    {
        /// <summary>
        /// Fake seachresult from boot24 datum.
        /// </summary>
        public FkSearchResult(string dataName) : base(() =>
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(
                new TextOf(
                    new ResourceOf($"Boot24/Datum/{dataName}", typeof(FkSearchResult))
                ).AsString()
            );
            return doc;
        })
        { }
    }
}
