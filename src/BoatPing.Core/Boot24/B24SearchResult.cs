using System;
using System.Linq;
using System.Xml.Linq;
using Xunit;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;
using Yaapii.Http;
using Yaapii.Http.Fake;
using Yaapii.Http.Parts.Bodies;
using Yaapii.Http.Parts.Uri;
using Yaapii.Http.Requests;
using Yaapii.Http.Responses;
using Yaapii.Http.Verifications;
using Yaapii.Http.Wires;
using Yaapii.JSON;
using Yaapii.Xml;

namespace BoatPing.Core.Boot24
{
    /// <summary>
    /// Search result from a Boot24.de search.
    /// </summary>
    public class B24SearchResult : XMLEnvelope
    {
        /// <summary>
        /// Search result from a Boot24.de search.
        /// </summary>
        public B24SearchResult(string searchName, int page, IWire httpWire) : base(new ScalarOf<IXML>(() =>
        {
            var cfg =
                new FirstOf<IXML>(
                    new XMLCursor(
                        new ResourceOf("Boot24/Datum/config.xml", typeof(B24SearchResult))
                    ).Nodes($"/searches/search[./title/text()='{searchName}']"),
                    new ArgumentException($"unknown_search_config:boot24:{searchName}")
                ).Value();

            var body =
                new Body.Of(
                        new Verified(
                            httpWire,
                            new Verification(
                                res => new Status.Of(res).AsInt() == 200,
                                res => new ApplicationException($"unexpected_status_code:boot24:{new Status.Of(res).AsInt()}")
                            )
                        ).Response(
                            new Post(
                                new Uri(new XMLString(cfg, "./root/text()").Value()),
                                new Mapped<IXML, IMapInput>(prm =>
                                    new QueryParam(
                                        new XMLString(prm, "./name/text()").Value(),
                                        new XMLString(prm, "./value/text()").Value()
                                    ),
                                    cfg.Nodes("./query/param")
                                ).ToArray()
                            )
                        )
                    );

            return
                new XMLCursor(
                    XDocument.Parse(
                        new Replaced(
                            new TextOf(
                                new FirstOf<string>(
                                    new JSONOf(
                                        body
                                    ).Values("$.result"),
                                    new ApplicationException($"unexpected_response:boot24:{new TextOf(body).AsString()}")
                                ).Value()
                            ),
                            "&CenterDot;",
                            "-"
                        ).AsString(),
                        LoadOptions.None
                    )
                );
        })
        )
        { }
    }
}
