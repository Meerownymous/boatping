using System;
using System.Collections.Generic;
using System.IO;
using Yaapii.Atoms.Enumerable;

namespace BoatPing.Core
{
    /// <summary>
    /// Searches which are configured in a file.
    /// </summary>
    public sealed class CfgSearches : ManyEnvelope
    {
        private const string COMMENT_CHAR = "#";
        private const string PRICE_PREFIX = "price";

        /// <summary>
        /// Searches which are configured in a file.
        /// </summary>
        public CfgSearches(Uri searchConfig, IList<string> knownPlatforms, Action<string> onError) : base(() =>
        {
            var result = new List<string>();
            var lines = File.ReadAllLines(searchConfig.AbsolutePath);
            foreach(var line in lines)
            {
                try
                {
                    if(!line.StartsWith(COMMENT_CHAR) && !line.ToLower().StartsWith(PRICE_PREFIX))
                    {
                        if(Uri.IsWellFormedUriString(line, UriKind.Absolute))
                        {
                            foreach (var platform in knownPlatforms)
                            {
                                if (line.ToLower().Contains(platform))
                                {
                                    result.Add(line);
                                }
                                else
                                {
                                    onError($"This page is not supported: {line}");
                                }
                            }
                        }
                        else
                        {
                            onError($"Invalid Url: {line}");
                        }
                    }

                }
                catch(Exception ex)
                {
                    onError($"Cannot understand search {line}: {ex.Message}");
                }
            }
            return result;
        })
        { }
    }
}
