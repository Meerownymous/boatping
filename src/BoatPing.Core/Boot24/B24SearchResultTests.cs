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
    public sealed class B24SearchResultTests
    {
        [Fact]
        public void ExtractsContent()
        {
            var str =
                new B24SearchResult(
                    "default-de",
                    1,
                    new FkWire(
                        200,
                        "",
                        new Body(new ResourceOf("Boot24/Datum/search.example-page-1.json", typeof(B24SearchResultTests)))
                    )
                )
                .AsNode()
                .ToString(System.Xml.Linq.SaveOptions.None);

            Assert.StartsWith(
                "<div class=\"main-content-norm\">",
                new B24SearchResult(
                    "default-de",
                    1,
                    new FkWire(
                        200,
                        "",
                        new Body(new ResourceOf("Boot24/Datum/search.example-page-1.json", typeof(B24SearchResultTests)))
                    )
                )
                .AsNode()
                .ToString(System.Xml.Linq.SaveOptions.None)
            );
        }
    }
}
