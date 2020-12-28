using System;
using Yaapii.Atoms;

namespace BoatPing.Core
{
    /// <summary>
    /// A kvp which has a fallback.
    /// </summary>
    public class KvpFallback : IKvp
    {
        private readonly string key;
        private readonly Func<string> value;
        private readonly string fallback;

        /// <summary>
        /// A kvp which has a fallback.
        /// </summary>
        public KvpFallback(string key, Func<string> value, string fallback)
        {
            this.key = key;
            this.value = value;
            this.fallback = fallback;
        }

        public bool IsLazy()
        {
            return true;
        }

        public string Key()
        {
            return key;
        }

        public string Value()
        {
            var result = fallback;
            try
            {
                result = value();
            }
            catch (Exception ex)
            {
                
            }
            return result;
        }
    }
}
