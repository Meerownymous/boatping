using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using BoatPing.Core.Model;
using Xive;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Xml;

namespace BoatPing.Core.LogBook
{
    public class XiveRecords : ListEnvelope<IAd>
    {
        public XiveRecords(IAd ad, IHive hive) : base(() =>
            {
                var area = hive.Shifted(ad.Source());
                IList<IAd> result = new List<IAd>();

                if (area.Catalog().Has(ad.ID()))
                {
                    var records = area.Comb(ad.ID()).Xocument("records.xml");

                    result =
                        new ListOf<IAd>(
                            new Yaapii.Atoms.Enumerable.Mapped<IXML, IAd>(record =>
                            {
                                var content = new Dictionary<string, string>();
                                foreach (var param in record.Nodes("./content/param"))
                                {
                                    content[new XMLString(param, "./name/text()").Value()] = new XMLString(param, "./value/text()").Value();
                                }
                                return
                                    new SimpleAd(
                                        new XMLString(record, "./id/text()").Value(),
                                        new XMLString(record, "./source/text()").Value(),
                                        new XMLString(record, "./url/text()").Value(),
                                        new XMLNumber(record, "./price/text()").AsDouble(),
                                        content
                                    );

                            },
                                records.Nodes("/records/record")
                            )
                        );
                }
                return result;
            },
            false
        )
        { }
    }
}
