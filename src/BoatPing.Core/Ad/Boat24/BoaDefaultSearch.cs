using System;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Ad.Boat24
{
    /// <summary>
    /// Defsault search for boat24
    /// 30.000€-59.000€
    /// 10m-14m
    /// Germany, Netherlands, Belgium, Denmark, France
    /// </summary>
    public class BoaDefaultSearch : TextEnvelope
    {
        public BoaDefaultSearch() : base(() =>
            "https://www.boat24.com/en/sailboats/?jhr_min=1970&lge_max=14&lge_min=10&page=0&prs_max=59000&prs_min=30000&rgo%5B0%5D=2&rgo%5B1%5D=41&rgo%5B2%5D=11&rgo%5B3%5D=15&rgo%5B4%5D=4&typ%5B0%5D=230&whr=EUR",
            false
        )
        { }
    }
}
