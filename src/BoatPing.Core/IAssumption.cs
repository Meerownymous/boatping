using System;
namespace BoatPing.Core
{
    /// <summary>
    /// An assumption.
    /// </summary>
    public interface IAssumption
    {
        bool IsTrue();
        bool IsFalse();
    }
}
