using System;
using System.IO;
using Xunit;
using Yaapii.Atoms.IO;

namespace BoatPing.Core.Cfg.Test
{
    public sealed class CfgIntervalTest
    {
        [Fact]
        public void ReadsToken()
        {
            using(var cfg = new TempFile())
            {
                File.WriteAllLines(
                    path: cfg.Value(),
                    new string[]
                    {
                        "fake-token-123",
                        "45"
                    }
                );

                Assert.Equal(
                    45,
                    new CfgInterval(
                        new Uri(cfg.Value()),
                        (error) => { }
                    )
                    .Value()
                    .TotalMinutes
                );
            }
        }

        [Fact]
        public void LogsInvalidConfig()
        {
            var result = "";
            using (var cfg = new TempFile())
            {
                File.WriteAllLines(
                    path: cfg.Value(),
                    new string[]
                    {
                        "too few lines" 
                    }
                );
                new CfgInterval(
                    new Uri(cfg.Value()),
                    (error) => { result = error; }
                ).Value();
            }

            Assert.NotEmpty(result);
        }
    }
}
