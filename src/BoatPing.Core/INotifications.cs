using System;
namespace BoatPing.Core
{
    public interface INotifications
    {
        void Post(INotification notification);
    }
}
