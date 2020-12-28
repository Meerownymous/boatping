using System;
using System.Collections.Generic;
using Yaapii.Atoms.Map;

namespace BoatPing.Core
{
    /// <summary>
    /// A fake ad.
    /// </summary>
    public class FkAd : IAd
    {
        private readonly string id;
        private readonly string url;
        private readonly double price;
        private readonly string source;
        private readonly IDictionary<string, string> content;

        /// <summary>
        /// A fake ad.
        /// </summary>
        public FkAd(string id, string url, double price, string source, IDictionary<string, string> content)
        {
            this.id = id;
            this.url = url;
            this.price = price;
            this.source = source;
            this.content = content;
        }

        public IDictionary<string, string> Content()
        {
            return new MapOf(this.content);
        }

        public string ID()
        {
            return this.id;
        }

        public double Price()
        {
            return this.price;
        }

        public string Source()
        {
            return this.source;
        }

        public string Url()
        {
            return this.url;
        }
    }
}
