using System;
using BoatPing.Core.Boot24;
using Xunit;

namespace BoatPing.Core.Ad.Boat24.Test
{
    public sealed class BoaNextUrlTests
    {
        [Fact]
        public void BuildsFromPageSegment()
        {
            Assert.Equal(
                "https://www.boat24.com/en/sailboats/?jhr_min=1970&lge_max=14&lge_min=10&page=20&prs_max=59000&prs_min=30000&rgo%5B0%5D=2&rgo%5B1%5D=41&rgo%5B2%5D=11&rgo%5B3%5D=15&rgo%5B4%5D=4&typ%5B0%5D=230&whr=EUR".ToLower(),
                new BoaNextUrl(new Uri("https://www.boat24.com/en/sailboats/?jhr_min=1970&lge_max=14&lge_min=10&page=0&prs_max=59000&prs_min=30000&rgo%5B0%5D=2&rgo%5B1%5D=41&rgo%5B2%5D=11&rgo%5B3%5D=15&rgo%5B4%5D=4&typ%5B0%5D=230&whr=EUR".ToLower()))
                    .Value()
                    .AbsoluteUri
            );
        }

        [Fact]
        public void BuildsWhenPageMissing()
        {
            Assert.Equal(
                "https://www.boat24.com/en/sailboats/?jhr_min=1970&lge_max=14&lge_min=10&prs_max=59000&prs_min=30000&rgo%5B0%5D=2&rgo%5B1%5D=41&rgo%5B2%5D=11&rgo%5B3%5D=15&rgo%5B4%5D=4&typ%5B0%5D=230&whr=EUR&page=20".ToLower(),
                new BoaNextUrl(new Uri("https://www.boat24.com/en/sailboats/?jhr_min=1970&lge_max=14&lge_min=10&prs_max=59000&prs_min=30000&rgo%5B0%5D=2&rgo%5B1%5D=41&rgo%5B2%5D=11&rgo%5B3%5D=15&rgo%5B4%5D=4&typ%5B0%5D=230&whr=EUR".ToLower()))
                    .Value()
                    .AbsoluteUri
            );
        }
    }
}