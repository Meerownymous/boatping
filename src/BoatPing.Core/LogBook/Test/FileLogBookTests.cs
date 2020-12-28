using System;
using System.Runtime.InteropServices;
using BoatPing.Core.Model;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Map;

namespace BoatPing.Core.LogBook.Test
{
    public sealed class FileLogBookTests
    {
        [Fact]
        public void Persists()
        {
            using(var dir = new TempDirectory())
            {
                var path = dir.Value().FullName;
                var ad = new SimpleAd("1", "xunit", "xunit://1.html", 100.0, new MapOf("some", "thing"));
                new FileLogBook(path).Record(ad);

                Assert.Equal(
                    1,
                    new FileLogBook(path).RecordsOf(ad).Count
                );
            }
        }
    }
}
