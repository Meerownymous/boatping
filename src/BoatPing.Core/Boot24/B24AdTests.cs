using System;
using System.Collections.Generic;
using System.Diagnostics;
using BoatPing.Core.Boot24.Datum;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core.Boot24
{
    public sealed class B24AdTests
    {
        [Fact]
        public void BuildsID()
        {        
            using (var search = new B24Page(new B24DefaultSearch()))
            using (var ad =
                new B24Ad(
                    new FirstOf<string>(
                        new B24PageAds(
                            search
                        )
                    ).Value(),
                    DateTime.Now
                )
            )
            {
                Assert.NotEmpty(ad.ID());
            };
        }

        [Fact]
        public void MovesForward()
        {
            var found = new List<string>();
            using (var search = new B24Page(new B24DefaultSearch()))
            {
                foreach (var url in new B24PageAds(search))
                {
                    using (var ad = new B24Ad(url, DateTime.Now))
                    {
                        found.Add($"{ad.Title()}:{ad.Price()}€:{ad.ID()}");
                    };
                }
            }

            Assert.Equal(
                new string[0],
                found.ToArray()
            );
        }


        [Fact]
        public void ExtractsPrice()
        {
            using (var search = new B24Page(new B24DefaultSearch()))
            using (var ad =
                new B24Ad(
                    new FirstOf<string>(
                        new B24PageAds(
                            search
                        )
                    ).Value(),
                    DateTime.Now
                )
            )
            {
                Assert.InRange(
                    ad.Price(),
                    1, int.MaxValue
                );
            }
        }
    }
}
