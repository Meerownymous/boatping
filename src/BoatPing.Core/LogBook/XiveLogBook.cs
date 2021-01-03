using System;
using System.Collections.Generic;
using BoatPing.Core.Model;
using Xive;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;
using Yaapii.Xambly;

namespace BoatPing.Core.LogBook
{
    /// <summary>
    /// A logbook based on xive.
    /// </summary>
    public class XiveLogBook : ILogBook
    {
        private readonly IHive xive;

        /// <summary>
        /// A logbook based on xive.
        /// </summary>
        public XiveLogBook(IHive xive)
        {
            this.xive = xive;
        }

        public void Record(IAd ad)
        {
            var createNew = false;
            var records = new XiveRecords(ad, this.xive);
            if (records.Count > 0)
            {
                var last = new LastOf<IAd>(records).Value();
                if(!new AdsEqual(last, ad))
                {
                    createNew = true;
                }
            }
            else
            {
                createNew = true;
            }
            if(createNew)
            {
                var doc =
                    this.xive
                        .Shifted(
                            ad.Source()
                        )
                        .Comb(ad.ID())
                        .Xocument("records.xml");

                var patch =
                    new Directives()
                        .Xpath("records")
                        .Add("record")
                        .Add("id").Set(ad.ID()).Up()
                        .Add("url").Set(ad.Url()).Up()
                        .Add("price").Set(new TextOf(ad.Price()).AsString()).Up()
                        .Add("source").Set(ad.Source()).Up();

                patch.Add("content");
                foreach (var key in ad.Content().Keys)
                {
                    patch.Add("param")
                        .Add("name").Set(key).Up()
                        .Add("value").Set(ad.Content()[key]).Up()
                        .Up();
                }
                doc.Modify(patch);
            }
        }

        public IList<IAd> RecordsOf(IAd ad)
        {
            return new XiveRecords(ad, this.xive);
        }

        public bool Contains(IAd ad)
        {
            return new XiveRecords(ad, this.xive).Count > 0;
        }

        public bool ContainsVersion(IAd ad)
        {
            var result = false;
            foreach(var record in new XiveRecords(ad, this.xive))
            {
                if(new AdsEqual(record, ad))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
