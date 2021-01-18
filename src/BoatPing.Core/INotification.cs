using System.Collections;
using BriX;

namespace BoatPing.Core
{
    /// <summary>
    /// A notification about news at the boat market.
    /// </summary>
    public interface INotification
    {
        string Title();
        string Event();
        IAd Ad();
        string Content();
    }
}