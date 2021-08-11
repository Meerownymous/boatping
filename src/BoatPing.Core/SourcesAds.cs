using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoatPing.Core.Ad.BandOfBoats;
using BoatPing.Core.Ad.Boat24;
using BoatPing.Core.Ad.Scanboat;
using BoatPing.Core.Ad.YachtAll;
using BoatPing.Core.Boot24;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace BoatPing.Core
{
    /// <summary>
    /// Source urls as ad lists.
    /// </summary>
    public sealed class SourcesAds : ManyEnvelope<IEnumerable<IAd>>
    {
        /// <summary>
        /// Source urls as ad lists.
        /// </summary>
        public SourcesAds(IEnumerable<string> sources, int minPrice, int maxPrice) : base(() =>
            {
                return
                    new Mapped<string, IEnumerable<IAd>>(
                        source =>
                        {
                            IEnumerable<IAd> result = new ManyOf<IAd>();
                            if (source.ToLower().Contains("yachtall.com"))
                            {
                                result = new YaaAds(source);
                            }
                            else if(source.ToLower().Contains("bandofboats.com"))
                            {
                                result = new BobAds(source);
                            }
                            else if (source.ToLower().Contains("boot24.com"))
                            {
                                result = new B24Ads(source, minPrice, maxPrice);
                            }
                            else if (source.ToLower().Contains("boat24.com"))
                            {
                                result = new BoaAds(source);
                            }
                            else if (source.ToLower().Contains("scanboat.com"))
                            {
                                result = new ScbAds(source, minPrice, maxPrice);
                            }
                            return result;
                        },
                        sources
                    );
            },
            true
        )
        { }
    }
}
