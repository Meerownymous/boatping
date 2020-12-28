using System;
using System.Collections.Generic;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.Model
{
    /// <summary>
    /// A simple ad.
    /// </summary>
    public class SimpleAd : IAd
    {
        private readonly string id;
        private readonly double price;
        private readonly string source;
        private readonly string url;
        private readonly IDictionary<string, string> content;

        /// <summary>
        /// A fake ad.
        /// </summary>
        public SimpleAd(string id, string source, string url, double price, IDictionary<string, string> content)
        {
            this.id = id;
            this.price = price;
            this.source = source;
            this.url = url;
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
