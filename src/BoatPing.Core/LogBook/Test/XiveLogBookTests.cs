using System;
using BoatPing.Core.Model;
using Xunit;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace BoatPing.Core.LogBook.Test
{

    public sealed class XiveLogBookTests
    {
        [Fact]
        public void RecordsAd()
        {
            var ad = new SimpleAd("1", "xunit", "xunit://1.html", 100.0, new MapOf("some", "thing"));
            var book = new XiveLogBook(new RamHive("root"));
            book.Record(ad);

            Assert.True(
                new AdsEqual(
                    ad,
                    new FirstOf<IAd>(
                        book.RecordsOf(ad)
                    ).Value()
                )
            );
        }

        [Fact]
        public void DoesNotRecordTwice()
        {
            var ad = new SimpleAd("1", "xunit", "xunit://1.html", 100.0, new MapOf("some", "thing"));
            var book = new XiveLogBook(new RamHive("root"));
            book.Record(ad);
            book.Record(ad);

            Assert.Equal(
                1,
                book.RecordsOf(ad).Count
            );
        }

        [Fact]
        public void RecordsIfDifferent()
        {
            var ad = new SimpleAd("1", "xunit", "xunit://1.html", 100.0, new MapOf("some", "thing"));
            var adChanged = new SimpleAd("1", "xunit", "xunit://1.html", 50, new MapOf("some", "thing"));
            var book = new XiveLogBook(new RamHive("root"));
            book.Record(ad);
            book.Record(adChanged);

            Assert.Equal(
                2,
                book.RecordsOf(ad).Count
            );
        }

        [Fact]
        public void KnowsItsRecordsIfEqual()
        {
            var ad = new SimpleAd("1", "xunit", "xunit://1.html", 100.0, new MapOf("some", "thing"));
            var adChanged = new SimpleAd("1", "xunit", "xunit://1377.html", 50, new MapOf("another", "thing"));
            var book = new XiveLogBook(new RamHive("root"));
            book.Record(ad);

            Assert.True(
                book.Contains(adChanged)
            );
        }

        [Fact]
        public void KnowsItsRecordsIfUnequal()
        {
            var ad = new SimpleAd("1", "xunit", "xunit://1.html", 100.0, new MapOf("some", "thing"));
            var otherAd = new SimpleAd("2", "xunit", "xunit://1.html", 50, new MapOf("some", "thing"));
            var book = new XiveLogBook(new RamHive("root"));
            book.Record(ad);

            Assert.False(
                book.Contains(otherAd)
            );
        }

        [Fact]
        public void KnowsItsExactRecordsIfEqual()
        {
            var ad = new SimpleAd("1", "xunit", "xunit://1.html", 100.0, new MapOf("some", "thing"));
            var book = new XiveLogBook(new RamHive("root"));
            book.Record(ad);

            Assert.True(
                book.ContainsVersion(ad)
            );
        }

        [Fact]
        public void KnowsItsExactRecordsIfUnequal()
        {
            var ad = new SimpleAd("1", "xunit", "xunit://1.html", 100.0, new MapOf("some", "thing"));
            var adChanged = new SimpleAd("1", "xunit", "xunit://1.html", 50, new MapOf("some", "thing"));
            var book = new XiveLogBook(new RamHive("root"));
            book.Record(ad);

            Assert.False(
                book.ContainsVersion(adChanged)
            );
        }

        [Fact]
        public void DiffersBySource()
        {
            var ad1 = new SimpleAd("1", "source-1", "xunit://1.html", 100.0, new MapOf("some", "thing"));
            var ad2 = new SimpleAd("1", "source-2", "xunit://1.html", 50, new MapOf("some", "thing"));
            var book = new XiveLogBook(new RamHive("root"));
            book.Record(ad1);
            book.Record(ad2);

            Assert.Equal(
                1,
                book.RecordsOf(ad1).Count
            );
        }
    }
}