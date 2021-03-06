using System;
using System.IO;
using Xunit;
using Yaapii.Atoms.IO;

namespace BoatPing.Core.Cfg.Test
{
    public sealed class CfgBotTest
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
                    "fake-token-123",
                    new CfgBot(new Uri(cfg.Value()), (error) => { }).AsString()
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
                new CfgBot(
                    new Uri(cfg.Value()),
                    (error) => { result = error; }
                ).AsString();
            }

            Assert.NotEmpty(result);
        }
    }
}
