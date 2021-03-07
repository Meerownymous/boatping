using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.IO;

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
                foreach (var line in lines)
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(line) && !line.StartsWith(COMMENT_CHAR) && !line.ToLower().StartsWith(PRICE_PREFIX))
                        {
                            if (Uri.IsWellFormedUriString(line, UriKind.Absolute))
                            {
                                var supported = false;
                                foreach (var platform in
                                    new Mapped<string, string>(
                                        pl => pl.ToLower(),
                                        knownPlatforms
                                    )
                                )
                                {
                                    if (line.Contains(platform.ToLower()))
                                    {
                                        supported = true;
                                        break;
                                    }
                                }

                                if (!supported)
                                {
                                    onError($"This page is not supported: {line}");
                                }
                                else
                                {
                                    result.Add(line);
                                }
                            }
                            else
                            {
                                if (line.StartsWith("http"))
                                {
                                    onError($"Invalid Url: {line}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        onError($"Cannot understand search {line}: {ex.Message}");
                    }
                }
                return result;
            },
            true
        )
        { }
    }
}
