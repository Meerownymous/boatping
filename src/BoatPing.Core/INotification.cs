using System.Collections;
using BriX;

namespace BoatPing.Core
{
    public interface INotification
    {
        string Title();
        string Event();
        IAd Ad();
    }
}