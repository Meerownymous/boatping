using System;
using System.Collections.Generic;
using BriX;

namespace BoatPing.Core.Model
{
    public interface IAd
    {
        string ID();
        DateTime Found();
        IBrix Printed();
        IEnumerable<string> Pix();
    }
}
